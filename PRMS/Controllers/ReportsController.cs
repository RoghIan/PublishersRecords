using API.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRMS.Data;
using PRMS.DTOs;
using PRMS.Entities;
using PRMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Controllers
{
    public class ReportsController : BaseApiController
    {
        private readonly IReportRepository _reportRepo;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IPublihserRepository _publihserRepository;

        public ReportsController(IReportRepository reportRepo, IMapper mapper, DataContext dataContext, IPublihserRepository publihserRepository)
        {
            _reportRepo = reportRepo;
            _mapper = mapper;
            _dataContext = dataContext;
            _publihserRepository = publihserRepository;
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

            var isNumeric = double.TryParse(reportDto.Hours, out double hours);

            if (isNumeric)
            {
                reportToAdd.Hours = hours;
                reportToAdd.HasParticipated = hours > 0;
            } else
            {
                reportToAdd.HasParticipated = reportDto.Hours == "Yes";
            }

            if (reportToAdd.ReportingAs == API.Enums.ReportingAs.AuxiliaryPioneer)
                reportToAdd.IsAuxi = true;

            _reportRepo.Add(reportToAdd);

            if (await _reportRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to add report");
        }

        [HttpPost("bulk-post")]
        public async Task<ActionResult> BulkPost([FromBody] List<ReportDto> reportDtoList)
        {
            var reports = reportDtoList.DistinctBy(x => x.PublisherId).ToList();

            var hasSavedReports = await _dataContext.Reports
                .Where(x => x.ReportDate.Month == DateTime.Now.AddMonths(-1).Month && x.ReportDate.Year == DateTime.Now.Year)
                .ToListAsync();

            var stringPublisherIds = hasSavedReports.Select(x => x.PublisherId).ToList().ConvertAll(i => i.ToString());

            var newReports = reports.ExceptBy(stringPublisherIds, x => x.PublisherId).ToList();

            foreach (var reportDto in newReports)
            {
                var reportToAdd = new Report
                {
                    PublisherId = int.Parse(reportDto.PublisherId),
                    BibleStudies = int.TryParse(reportDto.BibleStudies, out int studies) ? studies : 0,
                    ReportingAs = reportDto.ReportingAs,
                    ReportDate = (DateTime)(reportDto.ReportDate.HasValue ? reportDto.ReportDate : DateTime.Now.AddMonths(-1)),
                    CreatedDate = DateTime.Now,
                    Remarks = reportDto.Remarks,
                };

                var isNumeric = double.TryParse(reportDto.Hours, out double hours);
                bool hasNoReportHours = string.IsNullOrEmpty(reportDto.Hours.Trim());

                if (isNumeric)
                {
                    reportToAdd.Hours = hours;
                    reportToAdd.HasParticipated = hours > 0;

                }
                else if (hasNoReportHours) 
                {
                    bool hasParticipated = reportDto.Participated.Trim().ToLower() == "Yes".Trim().ToLower();
                    reportToAdd.HasParticipated = hasParticipated;
                }
                else
                {
                    bool hasParticipatedBasedOnHours = reportDto.Hours.Trim().ToLower() == "Yes".Trim().ToLower();
                    reportToAdd.HasParticipated = hasParticipatedBasedOnHours;
                }

                if (reportToAdd.ReportingAs == ReportingAs.AuxiliaryPioneer)
                    reportToAdd.IsAuxi = true;

                _reportRepo.Add(reportToAdd);
            }

            if (await _reportRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to add report");
        }

        [HttpPost("bulk-post-update-publisher")]
        public async Task<ActionResult> BulkPostAndUpdatePublisher([FromBody] List<ReportDto> reportDtoList)
        {
            var reports = reportDtoList.DistinctBy(x => x.PublisherId).ToList();

            var hasSavedReports = await _dataContext.Reports
                .Where(x => x.ReportDate.Month == DateTime.Now.AddMonths(-1).Month && x.ReportDate.Year == DateTime.Now.Year)
                .ToListAsync();

            var stringPublisherIds = hasSavedReports.Select(x => x.PublisherId).ToList().ConvertAll(i => i.ToString());

            var newReports = reports.ExceptBy(stringPublisherIds, x => x.PublisherId).ToList();

            foreach (var reportDto in newReports)
            {
                var reportToAdd = new Report
                {
                    PublisherId = int.Parse(reportDto.PublisherId),
                    BibleStudies = int.TryParse(reportDto.BibleStudies, out int studies) ? studies : 0,
                    ReportingAs = reportDto.ReportingAs,
                    ReportDate = (DateTime)(reportDto.ReportDate.HasValue ? reportDto.ReportDate : DateTime.Now.AddMonths(-1)),
                    CreatedDate = DateTime.Now,
                    Remarks = reportDto.Remarks,
                };

                var isNumeric = double.TryParse(reportDto.Hours, out double hours);
                bool hasNoReportHours = string.IsNullOrEmpty(reportDto.Hours.Trim());

                if (isNumeric)
                {
                    reportToAdd.Hours = hours;
                    reportToAdd.HasParticipated = hours > 0;

                }
                else if (hasNoReportHours)
                {
                    bool hasParticipated = reportDto.Participated.Trim().ToLower() == "Yes".Trim().ToLower();
                    reportToAdd.HasParticipated = hasParticipated;
                }
                else
                {
                    bool hasParticipatedBasedOnHours = reportDto.Hours.Trim().ToLower() == "Yes".Trim().ToLower();
                    reportToAdd.HasParticipated = hasParticipatedBasedOnHours;
                }

                if (reportToAdd.ReportingAs == ReportingAs.AuxiliaryPioneer)
                    reportToAdd.IsAuxi = true;

                _reportRepo.Add(reportToAdd);

                //PUBLISHER UPDATE

                var userToUpdate = await _publihserRepository.GetPublisherByIdAsync(int.Parse(reportDto.PublisherId));
                userToUpdate.BirthDate = reportDto.BirthDate;
                userToUpdate.BaptismDate = reportDto.BaptismDate;

                _publihserRepository.Update(userToUpdate);
            }

            if (await _reportRepo.SaveAllAsync() && await _publihserRepository.SaveAllAsync()) return NoContent();

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
