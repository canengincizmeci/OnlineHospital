using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineHospital.UI.Models;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentManagementController : Controller
    {
        private readonly HttpClient _httpClient;

        public AppointmentManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<IActionResult> GetDoctorListForAppointment()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_DoctorManagement/DoctorList");


            if (response.IsSuccessStatusCode)
            {
                var doctors = await response.Content.ReadFromJsonAsync<List<DoctorListViewModel>>();
                return View(doctors);
            }

            return View(new List<DoctorListViewModel>());
        }


        [HttpGet]
        public async Task<IActionResult> DoctorDetailsForAppointment(int doctorId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44365/api/API_AppointmentManagement/DoctorDetailsForAppointment?doctorId={doctorId}");
            Console.WriteLine($"Request URL: {response}");



            var jsonString = await response.Content.ReadAsStringAsync();
            var doctorDetails = JsonConvert.DeserializeObject<DoctorListViewModel>(jsonString);



            return View(doctorDetails);
        }
        //[HttpGet]
        //public async Task<IActionResult> AddAppointForDoctor(int doctorId)
        //{

        //    var response = await _httpClient.GetAsync($"https://localhost:44365/api/API_AppointmentManagement/IsThereAppointmentForDoctor?doctorId={doctorId}");

        //    var jsonString = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<AddAppointmentForDoctorViewModel>(jsonString);
        //    ViewBag.DoctorId = doctorId;
        //    return View(result);

        //}
        [HttpGet]
        public async Task<IActionResult> AddAppointForDoctor(int doctorId)
        {

            var response = await _httpClient.GetAsync($"https://localhost:44365/api/API_AppointmentManagement/IsThereAppointmentForDoctor?doctorId={doctorId}");

            if (!response.IsSuccessStatusCode)
            {

                return BadRequest("API çağrısı başarısız.");
            }


            var jsonString = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);


            var doctorDetails = new DoctorListViewModel
            {
                DoctorId = (int?)jsonObject.doctorId,
                DoctorName = (string)jsonObject.doctorName,
                IsCompletedInfos = (bool)jsonObject.isProfileUpdated,
                Specialty = (string)jsonObject.doctorSpecialty.specialtyName
            };


            var result = new AddAppointmentForDoctorViewModel
            {
                DoctorDetails = doctorDetails,
                Times = new List<DateTime>()
            };


            ViewBag.DoctorId = doctorId;

            return View(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddAppointForDoctor(int doctorId, string appointmentTime)
        {
            AddAppointmentPostModel model = new AddAppointmentPostModel
            {
                _doctorId = doctorId,
                _AppointmentDate = appointmentTime
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_AppointmentManagement/AddAppointmentForDoctor", model);

            if (response.IsSuccessStatusCode)
            {

                return Json(new { success = true, message = "Randevu Başarıyla Eklendi" });
            }
            else
            {
                return Json(new { success = true, message = "Randevu Eklenirken Bir hata oluştu" });
            }
        }



    }
}
