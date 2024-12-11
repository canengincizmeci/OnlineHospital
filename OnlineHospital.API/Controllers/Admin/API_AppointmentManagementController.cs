using Microsoft.AspNetCore.Mvc;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.Admin
{
    public class API_AppointmentManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public API_AppointmentManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddAppointment")]
        public async Task<IActionResult> AddAppointment()
        {




            return View();
        }
    }
}
