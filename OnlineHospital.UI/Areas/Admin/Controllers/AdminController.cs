using Microsoft.AspNetCore.Mvc;
using CommonLibrary.ViewModels;
using OnlineHospital.API.Controllers;
using OnlineHospital.DB.Model;
using CommonLibrary.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly HttpClient _httpClient;

        public AdminController(ILogger<AdminController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("HomeController");
        }

        public IActionResult AdminIndex()
        {


            return View();
        }


    }
}
