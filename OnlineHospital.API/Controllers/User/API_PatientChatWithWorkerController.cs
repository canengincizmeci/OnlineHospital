using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHospital.DB.Model;

namespace OnlineHospital.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_PatientChatWithWorkerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public API_PatientChatWithWorkerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("FindWorkerForChat")]
        public async Task<IActionResult> FindWorkerForChat()
        {
            var selectedWorker = await _context.PatientRelationsWorker.OrderBy(p => new Guid()).FirstOrDefaultAsync(p => p.IsProfileUpdated == true);



            return Ok(selectedWorker);
        }

    }
}
