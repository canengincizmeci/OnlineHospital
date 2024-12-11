using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorManagementController : Controller
    {
        private readonly HttpClient _httpClient;

        public DoctorManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> DoctorList()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_DoctorManagement/DoctorList");
           
          
            if (response.IsSuccessStatusCode)
            {
                var doctors = await response.Content.ReadFromJsonAsync<List<DoctorListViewModel>>();
                return View(doctors);
            }

            return View(new List<DoctorListViewModel>());
        }
    }
}
