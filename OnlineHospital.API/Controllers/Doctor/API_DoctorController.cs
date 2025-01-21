using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.Doctor
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public API_DoctorController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost("GetDoctorIdsByDoctorEmail")]
        public async Task<IActionResult> GetDoctorIdsByDoctorEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var doctor = await _context.Doctors.Include(l => l.User).FirstOrDefaultAsync(p => p.UserId == user!.Id);
            IdentityForRolesViewModel model = new IdentityForRolesViewModel
            {
                UserId = user!.Id,
                IdByRole = doctor!.DoctorId
            };
            return Ok(model);
        }





    }
}
