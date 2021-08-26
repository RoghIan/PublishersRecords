using Microsoft.AspNetCore.Mvc;
using PRMS.Data;
using PRMS.Entities;

namespace PRMS.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult<Publisher> GetNotFound()
        {
            var thing = _context.Publishers.Find(-1);

            if (thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Publishers.Find(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<Publisher> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }

    }
}
