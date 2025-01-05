using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.PatientRelationsWorker
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_PatientRelationsWorkerController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public API_PatientRelationsWorkerController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("GetWorkerIdsByAdminEmail")]
        public async Task<IActionResult> GetAdminIdsByAdminEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var admin = await _context.PatientRelationsWorker.Include(l => l.User).FirstOrDefaultAsync(p => p.UserId == user!.Id);
            IdentityForRolesViewModel model = new IdentityForRolesViewModel
            {
                IdByRole = admin!.WorkerId,
                UserId = user!.Id
            };


            return Ok(model);
        }



    }
}
