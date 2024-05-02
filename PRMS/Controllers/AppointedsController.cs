using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PRMS.Controllers;
using PRMS.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointedsController : BaseApiController
    {
        private readonly IAppointedRepository _appointedRepository;

        public AppointedsController(IAppointedRepository appointedRepository)
        {
            _appointedRepository = appointedRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointedDto>>> Get()
        {
            return Ok(await _appointedRepository.GetAppointedsAsync());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
