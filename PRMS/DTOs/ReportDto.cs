using API.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PRMS.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Hours { get; set; }
        public int Placement { get; set; } = 0;
        public int VideoShowings { get; set; } = 0;
        public int ReturnVisits { get; set; } = 0;
        public string BibleStudies { get; set; }
        public bool IsAuxi { get; set; }
        [EnumDataType(typeof(ReportingAs))]
        public ReportingAs ReportingAs { get; set; }
        public bool HasParticipated { get; set; }
        public DateTime? ReportDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string PublisherId { get; set; }
        public string Participated { get; set; }
    }
}
