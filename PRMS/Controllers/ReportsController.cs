using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PRMS.Data;
using PRMS.DTOs;
using PRMS.Entities;
using PRMS.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRMS.Controllers
{
    public class ReportsController : BaseApiController
    {
        private readonly IReportRepository _reportRepo;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public ReportsController(IReportRepository reportRepo, IMapper mapper, DataContext dataContext)
        {
            _reportRepo = reportRepo;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> Get()
        {
            var reports = await _reportRepo.GetReportsDtoAsync();

            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> Get(int id)
        {
            var report = await _reportRepo.GetReportDtoByIdAsync(id);

            return report;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReportDto reportDto)
        {
            var reportToAdd = _mapper.Map<ReportDto, Report>(reportDto);

            _reportRepo.Add(reportToAdd);

            if (await _reportRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to add report");
        }

        [HttpPost("bulk-post")]
        public async Task<ActionResult> BulkPost([FromBody] List<ReportDto> reportDtoList)
        {
            foreach (var reportDto in reportDtoList)
            {
                var reportToAdd = _mapper.Map<ReportDto, Report>(reportDto);

                _reportRepo.Add(reportToAdd);
            }

            if (await _reportRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to add report");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ReportDto reportDto)
        {
            var reportToUpdate = _mapper.Map<ReportDto, Report>(reportDto);

            _reportRepo.Update(reportToUpdate);

            if (await _reportRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update report");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _reportRepo.Delete(id);

            if (await _reportRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to delete report");
        }
    }
}
