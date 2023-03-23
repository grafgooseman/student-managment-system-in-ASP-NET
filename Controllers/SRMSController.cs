using AP_MT2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AG_MT2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SRMSController : ControllerBase
    {
        private readonly SRMSContext _context;

        public SRMSController(SRMSContext context)
        {
            _context = context;
        }

        [HttpGet("summary")]
        public ActionResult<object> GetSummary()
        {
            TotalCount tc = new TotalCount();
            tc.Students = _context.Courses.Count();
            tc.Courses = _context.Students.Count();

            return Ok(tc);
        }
    }

    public class TotalCount
    {
        public int Students { get; set; }
        public int Courses { get; set; }
    }
}
