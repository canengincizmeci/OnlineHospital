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
                var urlParts = redirectUrl.Split('/');

                if (urlParts.Length >= 2)
                {
                    var controller = urlParts[1];
                    var action = urlParts[2];


                    if (urlParts.Length >= 3)
                    {
                        if (controller.ToString() == "Admin")
                        {
                            var AdminResponse = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_Admin/GetAdminIdsByAdminEmail", request.Email);
                            if (AdminResponse.IsSuccessStatusCode)
                            {
                                var userInfos = await AdminResponse.Content.ReadFromJsonAsync<IdentityForRolesViewModel>();
                                HttpContext.Session.SetInt32("byRoleAdminId", userInfos!.IdByRole);
                                HttpContext.Session.SetString("AdminId", userInfos.UserId);
                            }
                        }
                        if (controller.ToString() == "Worker")
                        {
                            var WorkerResponse = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_PatientRelationsWorker/GetWorkerIdsByAdminEmail", request.Email);
                            if (WorkerResponse.IsSuccessStatusCode)
                            {
                                var userInfos = await WorkerResponse.Content.ReadFromJsonAsync<IdentityForRolesViewModel>();
                                HttpContext.Session.SetInt32("byRoleWorkerId", userInfos!.IdByRole);
                                HttpContext.Session.SetString("WorkerId", userInfos.UserId);
                            }
                        }
                        if (controller.ToString() == "Patient")
                        {
                            var Userresponse = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_User/GetUserIdsByUserEmail", request.Email);
                            if (Userresponse.IsSuccessStatusCode)
                            {
                                var userInfos = await Userresponse.Content.ReadFromJsonAsync<IdentityForRolesViewModel>();
                                HttpContext.Session.SetInt32("byRoleUserId", userInfos!.IdByRole);
                                HttpContext.Session.SetString("userId", userInfos.UserId);
                            }
                        }
                        if (controller.ToString() == "Doctor")
                        {
                            var doctorResponses = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_Doctor/GetDoctorIdsByDoctorEmail", request.Email);
                            if (doctorResponses.IsSuccessStatusCode)
                            {
                                var doctorInfos = await doctorResponses.Content.ReadFromJsonAsync<IdentityForRolesViewModel>();
                                HttpContext.Session.SetInt32("byDoctorId", doctorInfos!.IdByRole);
                                HttpContext.Session.SetString("doctorId", doctorInfos.UserId);
                            }
                        }
                        var area = urlParts[0];
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


