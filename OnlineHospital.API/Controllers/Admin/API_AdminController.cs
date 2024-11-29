
using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_AdminController : ControllerBase
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public API_AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
        }



    }
}
