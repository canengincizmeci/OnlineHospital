using CommonLibrary.Redis;
using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineHospital.DB.Model;
using System.Text.Json;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OnlineHospital.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_AppointmentManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public API_AppointmentManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("DoctorDetailsForAppointment")]
        public async Task<IActionResult> DoctorDetailsForAppointment(int doctorId)
        {
            var doctor = await _context.Doctors.Where(p => p.DoctorId == doctorId).Select(p => new DoctorListViewModel
            {
                DoctorId = p.DoctorId,
                DoctorName = p.DoctorName,
                IsCompletedInfos = p.IsProfileUpdated,
                Specialty = p.DoctorSpecialty.SpecialtyName
            }).FirstOrDefaultAsync();



            return Ok(doctor);
        }
        [HttpGet("IsThereAppointmentForDoctor")]
        public async Task<IActionResult> IsThereAppointmentForDoctor(int doctorId)
        {
            DateTime lastMonday = GetLastMonday().ToUniversalTime();

            bool IsThereAppointment = await _context.OpenAppointmentSlots.AnyAsync(p => (p.AppointmentDate == lastMonday && p.DoctorId == doctorId));

            if (IsThereAppointment)
            {
                var appointment = await _context.OpenAppointmentSlots.Where(p => (p.DoctorId == doctorId && p.AppointmentDate == lastMonday)).FirstOrDefaultAsync();

                var apponitments = await _context.OpenAppointmentSlots.Where(p => p.WeekForAppointmentId == appointment!.WeekForAppointmentId).ToListAsync();

                AddAppointmentForDoctorViewModel model = new AddAppointmentForDoctorViewModel
                {
                    Times = apponitments.Select(p => p.AppointmentDate).ToList(),
                    DoctorDetails = apponitments.Select(x => new DoctorListViewModel
                    {
                        DoctorId = x.DoctorId,
                        DoctorName = x.Doctor.DoctorName,
                        Specialty = x.Doctor.DoctorSpecialty.SpecialtyName
                    }).FirstOrDefault()!
                };
                return Ok(model);
            }
            else
            {
                var doctor = await _context.Doctors.Include(d => d.DoctorSpecialty).FirstOrDefaultAsync(p => p.DoctorId == doctorId);
                AddAppointmentForDoctorViewModel model = new AddAppointmentForDoctorViewModel
                {
                    DoctorDetails = new DoctorListViewModel
                    {
                        DoctorId = doctor.DoctorId,
                        DoctorName = doctor.DoctorName,
                        IsCompletedInfos = doctor.IsProfileUpdated,
                        Specialty = doctor.DoctorSpecialty.SpecialtyName
                    },
                    Times = new List<DateTime>()

                };
                return Ok(doctor);
            }

        }
        private DateTime GetLastMonday()
        {
            var today = DateTime.Now;
            int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;

            if (daysSinceMonday < 0)
            {
                daysSinceMonday += 7;
            }

            return today.AddDays(-daysSinceMonday).Date;
        }
        private DateTime GetDateTimeFormatForStringValue(string DateData)
        {
            string[] DateText = DateData.Split(" ");
            string DayText = DateText[0];
            string TimeText = DateText[1];

            DateTime now = DateTime.Now;


            DayOfWeek targetDay = DayText switch
            {
                "Pazartesi" => DayOfWeek.Monday,
                "Salı" => DayOfWeek.Tuesday,
                "Çarşamba" => DayOfWeek.Wednesday,
                "Perşembe" => DayOfWeek.Thursday,
                "Cuma" => DayOfWeek.Friday,
                "Cumartesi" => DayOfWeek.Saturday,
                "Pazar" => DayOfWeek.Sunday,
                _ => throw new ArgumentException("Geçersiz gün adı")
            };

            int daysDifference = (7 + (now.DayOfWeek - targetDay)) % 7;
            DateTime targetDate = now.AddDays(-daysDifference).Date;

            string startTime = TimeText.Split("-")[0]; 
            TimeSpan timeSpan = TimeSpan.Parse(startTime);
            targetDate = targetDate.Add(timeSpan);

            return targetDate;
        }
        [HttpPost("AddAppointmentForDoctor")]
        public async Task<IActionResult> AddAppointmentForDoctor([FromBody] AddAppointmentPostModel model)
        {
           
            DateTime dateTimeFormat = GetDateTimeFormatForStringValue(model._AppointmentDate);
            string formattedDateTime = dateTimeFormat.ToString("yyyy-MM-dd HH:mm:sszzz"); 

         
            var redisKey = $"appointment:doctor:{model._doctorId}";

            try
            {
              
                await RedisHelper.SetAsync(redisKey, formattedDateTime, TimeSpan.FromDays(7));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Redis işlemleri sırasında hata oluştu: {ex.Message}");
            }

         
            try
            {
                var timeDif = GetLastMonday();
                var isThereWeekValue = await _context.OpenAppointmentSlots
                    .OrderByDescending(p => p.WeekForAppointmentId)
                    .FirstOrDefaultAsync();

                if (isThereWeekValue == null || (timeDif - isThereWeekValue.AppointmentDate).TotalDays > 7)
                {
                   
                    await _context.WeekForAppointments.AddAsync(new()
                    {
                        WeekFirstDay = DateTime.UtcNow
                    });
                    await _context.SaveChangesAsync();

                    var currentWeekData = await _context.WeekForAppointments
                        .OrderByDescending(p => p.AppointmentWeekId)
                        .FirstOrDefaultAsync();

             
                    await _context.OpenAppointmentSlots.AddAsync(new()
                    {
                        AppointmentDate = dateTimeFormat.ToUniversalTime(),
                        DoctorId = model._doctorId,
                        WeekForAppointmentId = currentWeekData!.AppointmentWeekId
                    });
                    await _context.SaveChangesAsync();

                    return Ok("Yeni hafta ve randevu oluşturuldu.");
                }

             
                var _currentWeekData = await _context.WeekForAppointments
                    .OrderByDescending(p => p.AppointmentWeekId)
                    .FirstOrDefaultAsync();

                await _context.OpenAppointmentSlots.AddAsync(new()
                {
                    AppointmentDate = dateTimeFormat.ToUniversalTime(),
                    DoctorId = model._doctorId,
                    WeekForAppointmentId = _currentWeekData!.AppointmentWeekId
                });
                await _context.SaveChangesAsync();

                return Ok("Randevu başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Veritabanı işlemleri sırasında hata oluştu: {ex.Message}");
            }
        }


        //[HttpPost("AddAppointmentForDoctor")]
        //public async Task<IActionResult> AddAppointmentForDoctor(int _doctorId, string _AppointmentDate)
        //{
        //    // Tarih formatını düzenle
        //    DateTime dateTimeFormat = GetDateTimeFormatForStringValue(_AppointmentDate);

        //    // Redis anahtarı oluştur
        //    var redisKey = $"appointment:doctor:{_doctorId}";

        //    try
        //    {
        //        // Redis'ten mevcut randevuları al
        //        var existingAppointmentsJson = await RedisHelper.GetDateTimeAsync(redisKey);
        //        var appointments = new List<DateTime>();

        //        if (existingAppointmentsJson is not null)
        //        {
        //            try
        //            {
        //                // JSON'u listeye dönüştür
        //                appointments = JsonSerializer.Deserialize<List<DateTime>>(existingAppointmentsJson) ?? new List<DateTime>();
        //            }
        //            catch (JsonException)
        //            {
        //                // JSON deserialization hatası durumunda
        //                return StatusCode(500, "Redis'teki veri hatalı formatta.");
        //            }
        //        }

        //        // Yeni randevuyu ekle
        //        appointments.Add(dateTimeFormat);


        //        var updatedAppointmentsJson = JsonSerializer.Serialize(appointments);
        //        DateTime lastFormat = DateTime.Parse(updatedAppointmentsJson);
        //        await RedisHelper.SetDateTimeAsync(redisKey, lastFormat, TimeSpan.FromDays(7));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Redis işlemleri sırasında hata oluştu: {ex.Message}");
        //    }

        //    // Veritabanı işlemleri
        //    try
        //    {
        //        var timeDif = GetLastMonday();
        //        var isThereWeekValue = await _context.OpenAppointmentSlots
        //            .OrderByDescending(p => p.WeekForAppointmentId)
        //            .FirstOrDefaultAsync();

        //        if (isThereWeekValue == null || (timeDif - isThereWeekValue.AppointmentDate).TotalDays > 7)
        //        {
        //            // Yeni hafta oluştur
        //            await _context.WeekForAppointments.AddAsync(new()
        //            {
        //                WeekFirstDay = DateTime.UtcNow
        //            });
        //            await _context.SaveChangesAsync();

        //            var currentWeekData = await _context.WeekForAppointments
        //                .OrderByDescending(p => p.AppointmentWeekId)
        //                .FirstOrDefaultAsync();

        //            // Yeni randevu slotu ekle
        //            await _context.OpenAppointmentSlots.AddAsync(new()
        //            {
        //                AppointmentDate = dateTimeFormat,
        //                DoctorId = _doctorId,
        //                WeekForAppointmentId = currentWeekData!.AppointmentWeekId
        //            });
        //            await _context.SaveChangesAsync();

        //            return Ok("Yeni hafta ve randevu oluşturuldu.");
        //        }

        //        // Mevcut haftaya randevu ekle
        //        var _currentWeekData = await _context.WeekForAppointments
        //            .OrderByDescending(p => p.AppointmentWeekId)
        //            .FirstOrDefaultAsync();

        //        await _context.OpenAppointmentSlots.AddAsync(new()
        //        {
        //            AppointmentDate = dateTimeFormat,
        //            DoctorId = _doctorId,
        //            WeekForAppointmentId = _currentWeekData!.AppointmentWeekId
        //        });
        //        await _context.SaveChangesAsync();

        //        return Ok("Randevu başarıyla eklendi.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Veritabanı işlemleri sırasında hata oluştu: {ex.Message}");
        //    }
        //}

        //[HttpPost("AddAppointmentForDoctor")]
        //public async Task<IActionResult> AddAppointmentForDoctor(int _doctorId, string _AppointmentDate)
        //{
        //    DateTime dateTimeFormat = GetDateTimeFormatForStringValue(_AppointmentDate);

        //    var redisKey = $"appointment:doctor:{_doctorId}";

        //    try
        //    {
        //        var existingAppointmentsJson = await RedisHelper.GetDateTimeAsync(redisKey);
        //        var appointments = new List<DateTime>();

        //        if (existingAppointmentsJson is not null)
        //        {
        //            try
        //            {
        //                appointments = JsonSerializer.Deserialize<List<DateTime>>(existingAppointmentsJson) ?? new List<DateTime>();
        //            }
        //            catch (JsonException)
        //            {
        //                // Eğer veri tek bir tarihse, listeye dönüştür
        //                var singleDate = JsonSerializer.Deserialize<DateTime>(existingAppointmentsJson);
        //                appointments = new List<DateTime> { singleDate };
        //            }
        //        }

        //        appointments.Add(dateTimeFormat);

        //        var updatedAppointmentsJson = JsonSerializer.Serialize(appointments);
        //        await RedisHelper.SetDateTimeAsync(redisKey, updatedAppointmentsJson, TimeSpan.FromDays(7));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Redis'e yazma sırasında hata oluştu: {ex.Message}");
        //    }

        //    // Veritabanı işlemleri
        //    var timeDif = GetLastMonday();
        //    var isThereWeekValue = await _context.OpenAppointmentSlots.OrderByDescending(p => p.WeekForAppointmentId).FirstOrDefaultAsync();

        //    if ((timeDif - isThereWeekValue!.AppointmentDate).TotalDays > 7)
        //    {
        //        await _context.WeekForAppointments.AddAsync(new()
        //        {
        //            WeekFirstDay = DateTime.UtcNow
        //        });

        //        await _context.SaveChangesAsync();

        //        var currentWeekData = await _context.WeekForAppointments.OrderByDescending(p => p.AppointmentWeekId).FirstOrDefaultAsync();

        //        await _context.OpenAppointmentSlots.AddAsync(new()
        //        {
        //            AppointmentDate = dateTimeFormat,
        //            DoctorId = _doctorId,
        //            WeekForAppointmentId = currentWeekData!.AppointmentWeekId
        //        });

        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }

        //    var _currentWeekData = await _context.WeekForAppointments.OrderByDescending(p => p.AppointmentWeekId).FirstOrDefaultAsync();
        //    await _context.OpenAppointmentSlots.AddAsync(new()
        //    {
        //        AppointmentDate = dateTimeFormat,
        //        DoctorId = _doctorId,
        //        WeekForAppointmentId = _currentWeekData!.AppointmentWeekId
        //    });
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}

        //[HttpPost("AddAppointmentForDoctor")]
        //public async Task<IActionResult> AddAppointmentForDoctor(int _doctorId, string _AppointmentDate)
        //{
        //    DateTime dateTimeFormat = GetDateTimeFormatForStringValue(_AppointmentDate);


        //    var redisKey = $"appointment:doctor:{_doctorId}";



        //    try
        //    {
        //        var existingAppointmentsJson = await RedisHelper.GetDateTimeAsync(redisKey);
        //        var appointments = new List<DateTime>();

        //        if (existingAppointmentsJson is not null)
        //        {

        //            appointments = JsonSerializer.Deserialize<List<DateTime>>(existingAppointmentsJson) ?? new List<DateTime>();
        //        }

        //        appointments.Add(dateTimeFormat);


        //        var updatedAppointmentsJson = JsonSerializer.Serialize(appointments);
        //        await RedisHelper.SetDateTimeAsync(redisKey, dateTimeFormat, TimeSpan.FromDays(7));

        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, $"Redis'e yazma sırasında hata oluştu: {ex.Message}");
        //    }


        //    var timeDif = GetLastMonday();
        //    var isThereWeekValue = await _context.OpenAppointmentSlots.OrderByDescending(p => p.WeekForAppointmentId).FirstOrDefaultAsync();


        //    if ((timeDif - isThereWeekValue!.AppointmentDate).TotalDays > 7)
        //    {
        //        await _context.WeekForAppointments.AddAsync(new()
        //        {
        //            WeekFirstDay = DateTime.UtcNow
        //        });

        //        await _context.SaveChangesAsync();

        //        var currentWeekData = await _context.WeekForAppointments.OrderByDescending(p => p.AppointmentWeekId).FirstOrDefaultAsync();

        //        await _context.OpenAppointmentSlots.AddAsync(new()
        //        {
        //            AppointmentDate = dateTimeFormat,
        //            DoctorId = _doctorId,
        //            WeekForAppointmentId = currentWeekData!.AppointmentWeekId
        //        });

        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }

        //    var _currentWeekData = await _context.WeekForAppointments.OrderByDescending(p => p.AppointmentWeekId).FirstOrDefaultAsync();
        //    await _context.OpenAppointmentSlots.AddAsync(new()
        //    {
        //        AppointmentDate = dateTimeFormat,
        //        DoctorId = _doctorId,
        //        WeekForAppointmentId = _currentWeekData!.AppointmentWeekId
        //    });
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}





        //[HttpPost("AddAppointment")]
        //public async Task<IActionResult> AddAppointment(AppointmentViewModel request)
        //{
        //    await _context.OpenAppointmentSlots.AddAsync(new()
        //    {
        //        AppointmentDate = request.AppointmentDate,

        //    });
        //    await _context.SaveChangesAsync();
        //    var newAppointment = await _context.OpenAppointmentSlots.OrderByDescending(p => p.AppointmentId).FirstAsync();

        //    try
        //    {
        //        var redisDB = RedisHelper.GetDatabase();
        //        string redisKey = $"appointment:{newAppointment.AppointmentId}";
        //        string redisValue = JsonSerializer.Serialize(newAppointment);

        //        await redisDB.StringSetAsync(redisKey, redisValue, TimeSpan.FromDays(7));

        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, $"Redis hatası: {ex.Message}");
        //    }

        //    return Ok();
        //}


    }
}
