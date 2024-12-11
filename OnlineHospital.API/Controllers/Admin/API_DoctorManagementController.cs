using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.Admin
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

    public class API_DoctorManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public API_DoctorManagementController(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("DoctorList")]
        public async Task<IActionResult> DoctorList()
        {
            var doctors = await _context.Doctors.Where(p => p.ActivityStatus == true).Select(p => new DoctorListViewModel
            {
                DoctorName = p.DoctorName,
                IsCompletedInfos = p.IsProfileUpdated,
                Specialty = p.Specialty.SpecialtyName
            }).ToListAsync();

            return Ok(doctors);
        }
    }
}
