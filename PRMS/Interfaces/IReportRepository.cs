using PRMS.DTOs;
using PRMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRMS.Interfaces
{
    public interface IReportRepository
    {
        void Update(Report report);
        void Add(Report report);
        void Delete(int id);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<ReportDto>> GetReportsDtoAsync();
        Task<ReportDto> GetReportDtoByIdAsync(int id);
        Task<IEnumerable<Report>> GetReportsAsync();
        Task<Report> GetReportByIdAsync(int id);
    }
}
