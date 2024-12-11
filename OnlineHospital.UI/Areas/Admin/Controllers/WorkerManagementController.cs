using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkerManagementController : Controller
    {
        private readonly HttpClient _httpClient;

        public WorkerManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
