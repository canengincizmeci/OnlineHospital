using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CommonLibrary.ViewModels;

namespace OnlineHospital.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticsController : Controller
    {
        private readonly HttpClient _httpClient;

        public StatisticsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }



        public IActionResult StatisticsIndex()
        {
            return View();
        }
        public async Task<IActionResult> ExaminationCharts()
        {
            string apiUrl = "http://127.0.0.1:8000/api/results/";


            var response = await _httpClient.GetAsync(apiUrl);




            var jsonResponse = await response.Content.ReadAsStringAsync();


            var result = JsonSerializer.Deserialize<ExaminationChartDTO>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });



            return View(result);
        }
    }
}
