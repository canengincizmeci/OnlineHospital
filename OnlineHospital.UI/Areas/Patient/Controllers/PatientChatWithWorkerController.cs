using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnlineHospital.DB.Model;

namespace OnlineHospital.UI.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientChatWithWorkerController : Controller
    {
        private readonly HttpClient _httpClient;

        public PatientChatWithWorkerController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<IActionResult> UserChatPageIndex()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_PatientChatWithWorker/FindWorkerForChat");


            if (response.IsSuccessStatusCode)
            {
                var worker = await response.Content.ReadFromJsonAsync<DB.Model.PatientRelationsWorker>();
                return View(worker);
            }




            return View();
        }
    }
}
