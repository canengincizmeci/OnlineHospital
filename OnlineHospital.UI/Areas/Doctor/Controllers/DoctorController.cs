using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.UI.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class DoctorController : Controller
    {
        private readonly HttpClient _httpClient;

        public DoctorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult DoctorIndex()
        {
            return View();
        }
    }
}

