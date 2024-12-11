using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<JsonResult> DeletePatient(int patientId)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_PatientManagement/DeletePatient", patientId);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Üye Silindi" });

            }
            return Json(new { success = false, message = "Üye Silinemedi" });
        }
    }
}
