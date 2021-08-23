using System;

namespace PRMS.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public int Placement { get; set; }
        public int VideoShowings { get; set; }
        public int ReturnVisits { get; set; }
        public int BibleStudies { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publihser { get; set; }
    }
}
