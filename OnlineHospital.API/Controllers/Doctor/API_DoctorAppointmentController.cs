using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.Doctor
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_DoctorAppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public API_DoctorAppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("NewAppointments")]
        public async Task<IActionResult> NewAppointments()
        {
            var appointments = await _context.CreatedAppointments.Where(p => p.IsCompleted == false).Select(p => new NewAppointmentsDTO
            {
                appointmentDate = p.OpenAppointmentSlot.AppointmentDate,
                createdAppointmentId = p.CreatedAppointmentId,
                patientId = p.PatientId,
                patientName = p.Patient.PatientName
            }).ToListAsync();

            return Ok(appointments);
        }
        [HttpGet("AppointmentDetails")]
        public async Task<IActionResult> AppointmentDetails(int appointmentId)
        {
            var appointment = await _context.CreatedAppointments.Where(p => p.CreatedAppointmentId == appointmentId).Select(p => new NewAppointmentsDTO
            {
                appointmentDate = p.OpenAppointmentSlot.AppointmentDate,
                createdAppointmentId = p.CreatedAppointmentId,
                patientId = p.PatientId,
                patientName = p.Patient.PatientName
            }).FirstOrDefaultAsync();

            return Ok(appointment);
        }
        [HttpPost("AddResultForAppointment")]
        public async Task<IActionResult> AddResultForAppointment([FromBody] CompletedAppointmentDTO request)
        {
            var appointment = await _context.CreatedAppointments.Include(l => l.OpenAppointmentSlot).Include(k => k.Patient).FirstOrDefaultAsync(p => p.CreatedAppointmentId == request.createdAppointmentId);
            await _context.CompletedAppointments.AddAsync(new()
            {
                CompletedDate = DateTime.Now.ToUniversalTime(),
                Results = request.result,
                CreatedAppointmentId = request.createdAppointmentId
            });
            appointment!.IsCompleted = true;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
