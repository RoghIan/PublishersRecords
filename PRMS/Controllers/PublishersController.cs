using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRMS.Data;
using PRMS.DTOs;
using PRMS.Entities;
using PRMS.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRMS.Controllers
{
    public class PublishersController : BaseApiController
    {
        private readonly IPublihserRepository _publihserRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public PublishersController(IPublihserRepository publihserRepository, IMapper mapper, DataContext dataContext)
        {
            _publihserRepository = publihserRepository;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> Get()
        {
            var publishers = await _publihserRepository.GetPublishersDtoAsync();

            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> Get(int id)
        {
            var publisher = await _publihserRepository.GetPublisherDtoByIdAsync(id);

            return publisher;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PublisherAddDto publisherAddDto)
        {
            var publisherToAdd = _mapper.Map<PublisherAddDto, Publisher>(publisherAddDto);
            publisherToAdd.Appointeds = new List<Appointed>();

            var appointeds = await _dataContext.Appointeds.ToListAsync();

            foreach (var appointedId in publisherAddDto.AppointedIds)
            {
                var appointed = appointeds.Find(x => x.Id == appointedId);

                publisherToAdd.Appointeds.Add(appointed);
            }

            _publihserRepository.Add(publisherToAdd);

            if (await _publihserRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to add publisher");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PublisherUpdateDto publisherUpdateDto)
        {
            var userToUpdate = await _publihserRepository.GetPublisherByIdAsync(id);
            var appointeds = await _dataContext.Appointeds.ToListAsync();

            var updatedPublisher = _mapper.Map(publisherUpdateDto, userToUpdate);

            updatedPublisher.Appointeds = new List<Appointed>();

            foreach (var appointedId in publisherUpdateDto.AppointedIds)
            {
                var appointed = appointeds.Find(x => x.Id == appointedId);

                updatedPublisher.Appointeds.Add(appointed);
            }

            _publihserRepository.Update(updatedPublisher);

            if (await _publihserRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update publisher");
        }

        // DELETE api/<PublishersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _publihserRepository.Delete(id);

            if (await _publihserRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to delete publisher");
        }
    }
}
