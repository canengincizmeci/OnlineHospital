using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineHospital.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public API_RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName is null)
            {
                return BadRequest("Rol adı boş olamaz");
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("Bu rol zaten mevcut.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

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

       

    }
}
