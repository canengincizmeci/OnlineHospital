using Microsoft.AspNetCore.Mvc;

namespace OnlineHospital.UI.Areas.PatientRelationsWorker.Controllers
{
    [Area("PatientRelationsWorker")]
    public class WorkerController : Controller
    {
        private readonly HttpClient _httpClient;


        public WorkerController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

        }
     

       
        public IActionResult PatientRelationsWorkerIndex()
        {


            

            return View();
        }



    }
}

