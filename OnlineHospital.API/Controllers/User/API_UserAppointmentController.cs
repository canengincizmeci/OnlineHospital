using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_UserAppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public API_UserAppointmentController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private DateTime GetLastMonday()
        {
            var today = DateTime.Now;
            int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;

            if (daysSinceMonday < 0)
            {
                daysSinceMonday += 7;
            }

            return today.AddDays(-daysSinceMonday);
        }
        [HttpGet("AppointmentsForPatient")]
        public async Task<IActionResult> AppointmentsForPatient()
        {
            var lastWeek = await _context.WeekForAppointments.OrderByDescending(p => p.AppointmentWeekId).FirstOrDefaultAsync();
            var appointments = await _context.OpenAppointmentSlots.Include(l => l.WeekForAppointment).Where(p => (p.IsSelected == false && p.WeekForAppointment.WeekFirstDay == lastWeek!.WeekFirstDay)).Select(p => new AppointmentForPatientViewModel
            {
                AppointmentDate = p.AppointmentDate,
                DoctorId = p.DoctorId,
                DoctorName = p.Doctor.DoctorName,
                AppointmentId = p.AppointmentId
            }).ToListAsync();


            return Ok(appointments);
        }
        [HttpPost("MakeAppointment")]
        public async Task<IActionResult> MakeAppointment([FromBody] MakeAppointmentModel model)
        {

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == model.userId);
            var openAppointment = await _context.OpenAppointmentSlots.FirstOrDefaultAsync(p => p.AppointmentId == model.appointmentId);

            openAppointment!.IsSelected = true;
            await _context.CreatedAppointments.AddAsync(new()
            {
                OpenAppointmentSlotId = openAppointment.AppointmentId,
                PatientId = patient!.PatinetId
            });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
