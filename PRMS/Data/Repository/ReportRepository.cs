using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRMS.DTOs;
using PRMS.Entities;
using PRMS.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace PRMS.Data.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReportRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await _context.Reports.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ReportDto> GetReportDtoByIdAsync(int id)
        {
            return await _context.Reports.ProjectTo<ReportDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Report>> GetReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<IEnumerable<ReportDto>> GetReportsDtoAsync()
        {
            return await _context.Reports.ProjectTo<ReportDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void Add(Report report)
        {
            _context.Entry(report).State = EntityState.Added;
        }

        public void Update(Report report)
        {
            _context.Entry(report).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var report = _context.Reports.Find(id);

            _context.Entry(report).State = EntityState.Deleted;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
