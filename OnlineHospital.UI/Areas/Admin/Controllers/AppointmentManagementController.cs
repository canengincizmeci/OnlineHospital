using CommonLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public async Task<IActionResult> AddAppointment()
        {
            var response = await _httpClient.GetAsync("https://localhost:44365/api/API_AppointmentManagement/GetDoctorListForAppointment");
            if (response.IsSuccessStatusCode)
            {
                var doctors = await response.Content.ReadFromJsonAsync<List<SelectListItem>>();
                ViewBag.Doctors = new SelectList(doctors, "Value", "Text");

            }

            return View();
        }
        //[HttpPost]
        //public async Task<JsonResult> AddAppointment(AppointmentViewModel request)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("https://localhost:44365/api/API_AppointmentManagement/AddAppointment", request);
        //    if (response.IsSuccessStatusCode)
        //    {

        //        return Json(new { success = true, message = "Randevu Başarıyla Eklendi" });

        //    }

        //    return Json(new { success = false, message = "Randevu Eklenemedi" });
        //}
    }
}
