using Microsoft.AspNetCore.Mvc;
using OnlineHospital.DB.Model;

namespace OnlineHospital.UI.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientController : Controller
    {
        private readonly HttpClient _httpClient;

        public PatientController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult PatientIndex()
        {

            return View();
        }
    }
}
