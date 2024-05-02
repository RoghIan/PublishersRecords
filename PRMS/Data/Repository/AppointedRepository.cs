using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PRMS.Data;
using PRMS.DTOs;
using PRMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class AppointedRepository : IAppointedRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AppointedRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointedDto>> GetAppointedsAsync()
        {
            return await _context.Appointeds.ProjectTo<AppointedDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
