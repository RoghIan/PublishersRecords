using System;
using System.Collections.Generic;

namespace PRMS.DTOs
{
    public class PublisherAddDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BaptismDate { get; set; }
        public int ContactNumber { get; set; }
        public string Gender { get; set; }
        public int GroupId { get; set; }
        public bool IsActive { get; set; } = true;
        public IEnumerable<int> AppointedIds { get; set; }
    }
}
