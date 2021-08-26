using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PRMS.DTOs;
using PRMS.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRMS.Controllers
{
    public class PublishersController : BaseApiController
    {
        private readonly IPublihserRepository _publihserRepository;
        private readonly IMapper _mapper;

        public PublishersController(IPublihserRepository publihserRepository,IMapper mapper)
        {
            _publihserRepository = publihserRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> Get()
        {
            var publishers = await _publihserRepository.GetPublishersAsync();

            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> Get(int id)
        {
            var publisher = await _publihserRepository.GetPublisherByIdAsync(id);

            return publisher;
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
