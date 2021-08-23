using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRMS.Data;
using PRMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly DataContext _context;

        public PublishersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> Get()
        {
            var publishers = await _context.Publishers.ToListAsync();

            return publishers;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> Get(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        // POST api/<PublishersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PublishersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PublishersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
