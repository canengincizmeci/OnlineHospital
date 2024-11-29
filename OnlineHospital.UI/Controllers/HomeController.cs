using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnlineHospital.API.Controllers;
using OnlineHospital.UI.Models;
using System.Diagnostics;
using CommonLibrary.Extensions;
using System.Net.Http.Json;
using OnlineHospital.DB.Model;
using Humanizer;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.CodeAnalysis.Scripting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Data;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml.Linq;
using System;

namespace OnlineHospital.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;


        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;

            _httpClient = httpClientFactory.CreateClient("HomeController");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:44365/api/AccountHome/Login/", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                if (errorContent != null && errorContent.ContainsKey("error"))
                {
                    ModelState.AddModelError(string.Empty, errorContent["error"]);
                }
                return View();
            }

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (result != null && result.ContainsKey("redirectUrl"))
            {
                var redirectUrl = result["redirectUrl"];
                var urlParts = redirectUrl.Split('/');  // URL'yi "/" ile bölüyoruz

                if (urlParts.Length >= 2)
                {
                    var controller = urlParts[1];  // "Admin" kýsmý
                    var action = urlParts[2];      // "AdminIndex" kýsmý

                    // Eðer URL'de area da varsa, bunu da parametre olarak ekleyebilirsiniz.
                    if (urlParts.Length >= 3)
                    {
                        var area = urlParts.Length > 3 ? urlParts[3] : null;
                        return RedirectToAction(action, controller, new { area = area });
                    }
                    return RedirectToAction(action, controller);
                }
            }

          
            

            ModelState.AddModelError(string.Empty, "Bir hata oluþtu.");
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _httpClient.PostAsJsonAsync($"https://localhost:44365/api/AccountHome/SignUp", request);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kayýt iþlemi baþarýlý";
                return RedirectToAction(nameof(Login), "Home");
            }

            var errorContent = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            ModelState.AddModelError("", string.Join("\n", errorContent!.Errors));



            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


