using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineHospital.DB.Model;

namespace OnlineHospital.UI.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class AppointmentsForPatientController : Controller
    {
        private readonly HttpClient _httpClient;


        public AppointmentsForPatientController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

        }
        public async Task<IActionResult> AppointmentsForPatient()
        {
            int? patientId = HttpContext.Session.GetInt32("byRoleUserId");
            if (!patientId.HasValue)
            {
                return RedirectToAction("Login", "Home");

            }
            ViewBag.UserId = patientId;
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_UserAppointment/AppointmentsForPatient");

            if (response.IsSuccessStatusCode)
            {
                var appointments = await response.Content.ReadFromJsonAsync<List<AppointmentForPatientViewModel>>();
                return View(appointments);
            }

            return View(new List<AppointmentForPatientViewModel>());
        }
        [HttpPost]
        public async Task<IActionResult> MakeAppointment([FromBody] MakeAppointmentModel model)
        {
            string patientId = HttpContext.Session.GetString("userId")!;
            if (patientId is null)
            {
                return RedirectToAction("Login", "Home");

            }
            model.userId = patientId;
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_UserAppointment/MakeAppointment", model);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Randevu Başarıyla alındı" });
            }
            else
            {
                return Json(new { success = false, message = "Randevu atanırken bir sorun oldu" });
            }
        }
    }
}
