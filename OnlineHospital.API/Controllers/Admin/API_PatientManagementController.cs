using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.Admin
{


    [Route("api/[controller]")]
    [ApiController]
    public class API_PatientManagementController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public API_PatientManagementController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("PatientList")]
        public async Task<IActionResult> PatientList()
        {
            var patients = await _context.Patients.Where(p => (p.ActivityStatus == true && p.IsProfileUpdated == true)).Select(p => new PatientViewModel
            {
                BirthDate = p.BirthYear,
                UserId = p.UserId,
                UserName = p.User.UserName!
            }).ToListAsync();

            return Ok(patients);
        }
        [HttpPost("DeletePatient")]
        public async Task<IActionResult> DeletePatient([FromBody] string patientId)
        {
            if (patientId is null)
            {
                return BadRequest();

            }
            var user = await _userManager.FindByIdAsync(patientId);
            if (user is null)
            {
                return BadRequest();
            }
            user.ActivityStatus = false;
            var deletedUser = await _context.Patients.FindAsync(user.Id);
            deletedUser!.ActivityStatus = false;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
