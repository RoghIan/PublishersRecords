using PRMS.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRMS.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BaptismDate { get; set; }
        public int ContactNumber { get; set; }
        public string Gender { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Appointed> Appointeds { get; set; }
        [NotMapped]
        public IEnumerable<int> AppointedIds { get; set; }
        public bool IsActive { get; set; }
    }
}
