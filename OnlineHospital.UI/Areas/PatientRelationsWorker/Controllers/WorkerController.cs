using Microsoft.AspNetCore.Mvc;
using OnlineHospital.DB.Model;

namespace OnlineHospital.UI.Areas.PatientRelationsWorker.Controllers
{
    [Area("PatientRelationsWorker")]
    public class WorkerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public WorkerController(IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _context = context;
        }



        public async Task<IActionResult> PatientRelationsWorkerIndex()
        {
            int? id = HttpContext.Session.GetInt32("byRoleWorkerId");
            if (!id.HasValue)
            {
                return RedirectToAction("Login", "Home");
            }

            var worker = await _context.PatientRelationsWorker.FindAsync(id);
            ViewBag.WorkerName = worker!.WorkerName;


            return View();
        }



    }
}

