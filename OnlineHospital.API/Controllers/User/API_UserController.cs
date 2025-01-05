using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public API_UserController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost("GetUserIdsByUserEmail")]
        public async Task<IActionResult> GetUserIdsByUserEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var patient = await _context.Patients.Include(l => l.User).FirstOrDefaultAsync(p => p.UserId == user!.Id);
            IdentityForRolesViewModel model = new IdentityForRolesViewModel
            {
                IdByRole = patient!.PatinetId,
                UserId = user!.Id
            };

            return Ok(model);
        }
    }
}
