using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineHospital.DB.Model;

namespace OnlineHospital.UI.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class DoctorAppointmentController : Controller
    {
        private readonly HttpClient _httpClient;

        public DoctorAppointmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> NewAppointments()
        {
            var response = await _httpClient.GetAsync($"https://localhost:44365/api/API_DoctorAppointment/NewAppointments");




            var jsonString = await response.Content.ReadAsStringAsync();
            var appointments = JsonConvert.DeserializeObject<List<NewAppointmentsDTO>>(jsonString);



            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> AddResultForAppointment(int appointmentId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44365/api/API_DoctorAppointment/AppointmentDetails?appointmentId={appointmentId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<NewAppointmentsDTO>(jsonString);
            ViewBag.AppointmentId = appointmentId;
            return View(appointment);
        }
        [HttpPost]
        public async Task<IActionResult> AddResultForAppointment(CompletedAppointmentDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_DoctorAppointment/AddResultForAppointment", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DoctorIndex", "Doctor", new { area = "Doctor" });
            }
            else
            {
                return RedirectToAction("AddResultForAppointment", "Doctor", new { appointmentId = request.createdAppointmentId });
            }
        }
    }
}
