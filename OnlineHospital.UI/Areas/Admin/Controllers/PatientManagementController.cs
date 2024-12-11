using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    public class PatientManagementController : Controller
    {
        private readonly HttpClient _httpClient;

        public PatientManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> PatientList()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_PatientManagement/PatientList");

            if (response.IsSuccessStatusCode)
            {
                var patients = await response.Content.ReadFromJsonAsync<List<PatientViewModel>>();
                return View(patients);
            }


            return View(new List<PatientViewModel>());
        }
    }
}
