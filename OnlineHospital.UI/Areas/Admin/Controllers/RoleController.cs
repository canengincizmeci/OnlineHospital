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
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/Role/AddRole", new { roleName });
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rol başarıyla eklendi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Rol eklenirken bir hata oluştu.";
            }
            return RedirectToAction(nameof(AddRole));



        }
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/Role/GetRoles");
            if (response.IsSuccessStatusCode)
            {
                var roles = await response.Content.ReadFromJsonAsync<List<string>>();
                return View(roles);
            }

            TempData["ErrorMessage"] = "Roller yüklenemedi.";
            return View(new List<string>());
        }
    }
}
