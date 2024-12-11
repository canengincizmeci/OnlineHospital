using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly HttpClient _httpClient;

        public RoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }



        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_Role/AddRole", roleName);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rol başarıyla eklendi.";
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = "Rol eklenirken bir hata oluştu.";
            }
            return RedirectToAction("AdminIndex", "Admin");



        }
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_Role/GetRoles");
            if (response.IsSuccessStatusCode)
            {
                var roles = await response.Content.ReadFromJsonAsync<List<string>>();
                return View(roles);
            }

            TempData["ErrorMessage"] = "Roller yüklenemedi.";
            return View(new List<string>());
        }

        [HttpGet]
        public async Task<IActionResult> UsersListWithoutRole()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_Role/UsersListWithoutRoleAsync");
            if (response.IsSuccessStatusCode)
            {

                var nonRoleUsers = await response.Content.ReadFromJsonAsync<List<UsersWithoutRoleViewModel>>();
                return View(nonRoleUsers);
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleRequest request)
        {
            Console.WriteLine($"UserId: {request.UserId}, RoleName: {request.RoleName}");
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_Role/AssignRoleToUserAsync", request);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Rol başarıyla atandı" });
            }



            return Json(new { success = false, message = "Rol atanamadı" });
        }
    }
}
