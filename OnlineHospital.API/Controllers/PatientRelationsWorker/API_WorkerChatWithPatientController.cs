using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.API.Controllers.PatientRelationsWorker
{
    public class API_WorkerChatWithPatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
