
using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class API_AdminController : ControllerBase
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public API_AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpPost("GetAdminIdsByAdminEmail")]
        public async Task<IActionResult> GetAdminIdsByAdminEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var admin = await _context.Administrators.Include(l => l.User).FirstOrDefaultAsync(p => p.UserId == user!.Id);
            IdentityForRolesViewModel model = new IdentityForRolesViewModel
            {
                IdByRole = admin!.Id,
                UserId = user!.Id
            };


            return Ok(model);
        }


    }
}
