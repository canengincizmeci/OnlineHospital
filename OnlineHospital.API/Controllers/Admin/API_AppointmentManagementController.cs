using CommonLibrary.Redis;
using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;
using System.Text.Json;

namespace OnlineHospital.API.Controllers.Admin
{
    public class API_AppointmentManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public API_AppointmentManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        [HttpGet("GetDoctorListAppointment")]
        public async Task<IActionResult> GetDoctorListForAppointment()
        {
            List<SelectListItem> doctors = (from i in await _context.Doctors.ToListAsync()
                                            select new SelectListItem
                                            {
                                                Text = i.DoctorName,
                                                Value = i.DoctorId.ToString()
                                            }).ToList();
            return Ok(doctors);
        }
    }
}
