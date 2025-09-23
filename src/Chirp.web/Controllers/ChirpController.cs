using Microsoft.AspNetCore.Mvc;
using SimpleDB;
using Chirp.CLI;

namespace Chirp.Web.Controllers
{
    [ApiController]
    [Route("chirp")]
    public class ChirpController : ControllerBase
    {
        private readonly CSVDatabase<Cheep> _db;

        public ChirpController()
        {
            _db = CSVDatabase<Cheep>.Instance;
        }

        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] int? limit = null)
        {
            var cheeps = _db.Read(limit);
            return Ok(cheeps);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Cheep cheep)
        {
            _db.Store(cheep);
            return Ok(new { message = "Cheep added!", cheep });
        }
    }
}