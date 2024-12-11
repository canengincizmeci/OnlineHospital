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



    public class API_RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public API_RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] string roleName)
        {
            if (roleName is null)
            {
                return BadRequest("Rol adı boş olamaz");
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("Bu rol zaten mevcut.");
            }

            var result = await _roleManager.CreateAsync(new AppRole { Name = roleName });

            if (result.Succeeded)
            {
                return Ok($"Rol başrıyla oluşturuldu: {roleName}");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("GetRole")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return Ok(roles);
        }

        [HttpGet("UsersListWithoutRoleAsync")]
        public async Task<IActionResult> UsersListWithoutRoleAsync()
        {
            var nonRoleUsers = await _userManager.Users.Where(p => p.HasRole == false).Select(p => new UsersWithoutRoleViewModel
            {
                userId = p.Id,
                userName = p.UserName!,
                Roles = _roleManager.Roles.Select(l => l.Name).ToList()!
            }).ToListAsync();


            return Ok(nonRoleUsers);
        }
        [HttpPost("AssignRoleToUserAsync")]
        public async Task<IActionResult> AssignRoleToUserAsync([FromBody] AssignRoleRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == request.UserId);

            if (user is null)
            {
                return BadRequest();
            }
            if (!await _roleManager.RoleExistsAsync(request.RoleName))
            {
                return BadRequest("Geçersiz rol adı. Bu rol sistemde mevcut değil.");
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (result.Succeeded)
            {

                user.HasRole = true;
                if (request.RoleName == "Doctor")
                {
                    await _context.Doctors.AddAsync(new()
                    {
                        ActivityStatus = true,
                        BirthYear = null,
                        DoctorName = user.UserName!,
                        UserId = user.Id,
                        IsProfileUpdated = false,
                        MedicalSpecialtyId = 1
                    });
                }
                else if (request.RoleName == "Patient")
                {
                    await _context.Patients.AddAsync(new()
                    {
                        ActivityStatus = true,
                        BirthYear = null,
                        IsProfileUpdated = false,
                        UserId = user.Id,
                        PatientName = user.UserName!
                    });
                }
                else if (request.RoleName == "PatientRelationsWorker")
                {
                    await _context.PatientRelationsWorker.AddAsync(new()
                    {
                        ActivityStatus = false,
                        Availability = false,
                        BirthYear = null,
                        IsProfileUpdated = false,
                        WorkerName = user.UserName!,
                        UserId = user.Id
                    });
                }
                else if (request.RoleName == "Admin")
                {
                    await _context.Administrators.AddAsync(new()
                    {
                        AdminId = user.Id,

                    });
                }
                await _context.SaveChangesAsync();
                return Ok("Rol başarıyla atandı");
            }

            return Ok();
        }

    }
}
