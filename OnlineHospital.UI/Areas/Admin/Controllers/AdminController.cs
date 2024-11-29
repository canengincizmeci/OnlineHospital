using Microsoft.AspNetCore.Mvc;
using CommonLibrary.ViewModels;
using OnlineHospital.API.Controllers;
using OnlineHospital.DB.Model;
using CommonLibrary.Extensions;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly AccountHomeController _homeController;

        public AdminController(AccountHomeController homeController)
        {
            _homeController = homeController;
        }

       




        public IActionResult AdminIndex()
        {


            return View();
        }
    }
}
