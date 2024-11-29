using CommonLibrary.Extensions;
using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineHospital.DB.Model;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;


namespace OnlineHospital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountHomeController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountHomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel request)
        {


            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                return BadRequest(new { Errors = errors });
            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, Email = request.UserEmail }, request.PasswordConfirm);

            if (!identityResult.Succeeded)
            {

                ModelState.AddModelErrorList(identityResult.Errors);

                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errors });
            }





            return Ok(new { Message = "Kullanıcı başarıyla kaydedildi.", redirectUrl = "/Home/Login" });

        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                ModelState.AddModelError("Email", "Email veya şifre yanlış");
                return BadRequest(ModelState);
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 Dakika boyunca giriş yapamazsınız" });
                return BadRequest(ModelState);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Şifre yanlış");
                return BadRequest(ModelState);
            }

            var roles = await _userManager.GetRolesAsync(user);

           

            string url = FindIndexUrl(roles.ToList());
            if (url == "role bulunamadı")
            {
                return BadRequest(new { error = "Yetkiniz yok" });
            }

            return Ok(new { redirectUrl = url });
        }



        private async Task<AppUser> HasUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user!;
        }



        private async Task<bool> FindRoleForUserAsync(AppUser _user, string role)
        {
            bool hasRole = await _userManager.IsInRoleAsync(_user, role);

            return hasRole;
        }

        private async Task<List<string>> GetRoleListAsync(AppUser _user)
        {
            var roleList = await _userManager.GetRolesAsync(_user);

            return roleList.ToList();
        }
        private string FindIndexUrl(List<string> roles)
        {
            if (roles.Contains("Admin"))
            {
                return "/Admin/AdminIndex";
            }
            if (roles.Contains("Doctor"))
            {
                return "/Doctor/DoctorIndex";
            }
            if (roles.Contains("Patient"))
            {
                return "/Patient/PatientIndex";
            }
            if (roles.Contains("PatientRelationsWorker"))
            {
                return "/Worker/PatientRelationsWorkerIndex";
            }
            return "role bulunamadı";
        }

        //private string FindIndexUrl(List<string> roles)
        //{

        //    if (roles.Contains("Admin"))
        //    {
        //        return "AdminIndex Admin Admin";
        //    }
        //    if (roles.Contains("Doctor"))
        //    {
        //        return "DoctorIndex Doctor Doctor";
        //    }
        //    if (roles.Contains("Patient"))
        //    {
        //        return "PatientIndex Patinet Patinet";
        //    }
        //    if (roles.Contains("PatientRelationsWorker"))
        //    {
        //        return "WorkerIndex Worker PatientRelationsWorker";
        //    }
        //    return "role bulunamadı";
        //}








    }
}
