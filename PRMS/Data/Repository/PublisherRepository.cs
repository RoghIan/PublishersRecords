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

        public async Task<PublisherDto> GetPublisherDtoByIdAsync(int id)
        {
            return await _context.Publishers.ProjectTo<PublisherDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersDtoAsync()
        {
            return await _context.Publishers.ProjectTo<PublisherDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return await _context.Publishers.Include(x => x.Appointeds).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public void Add(Publisher publisher)
        {
            _context.Entry(publisher).State = EntityState.Added;
        }

        public void Update(Publisher publisher)
        {
            _context.Entry(publisher).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var publisher = _context.Publishers.Find(id);

            _context.Entry(publisher).State = EntityState.Deleted;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
