using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PRMS.DTOs;
using PRMS.Entities;
using PRMS.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRMS.Data.Repository
{
    public class PublisherRepository : IPublihserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PublisherRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PublisherDto> GetPublisherByIdAsync(int id)
        {
            return await _context.Publishers.ProjectTo<PublisherDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersAsync()
        {
            return await _context.Publishers.ProjectTo<PublisherDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Publisher publisher)
        {
            _context.Entry(publisher).State = EntityState.Modified;
        }
    }
}
