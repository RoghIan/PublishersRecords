using System;

namespace PRMS.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public double Hours { get; set; } = 0;
        public int Placement { get; set; } = 0;
        public int VideoShowings { get; set; } = 0;
        public int ReturnVisits { get; set; } = 0;
        public int BibleStudies { get; set; } = 0;
        public DateTime? ReportDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int PublisherId { get; set; }
    }
}
