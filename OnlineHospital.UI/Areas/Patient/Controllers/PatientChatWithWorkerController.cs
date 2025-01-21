using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnlineHospital.DB.Model;

namespace OnlineHospital.UI.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientChatWithWorkerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public PatientChatWithWorkerController(IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _context = context;
        }


        public async Task<IActionResult> UserChatPageIndex()
        {
            int? id = HttpContext.Session.GetInt32("byRoleUserId");
            if (!id.HasValue)
            {
                return RedirectToAction("Login", "Home");
            }
            var user = await _context.Patients.FindAsync(id);
            ViewBag.PatientName = user!.PatientName;
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
