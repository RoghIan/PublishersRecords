using System.Collections.Generic;

namespace PRMS.Entities
{
    public class Appointed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Publisher> Publishers { get; set; }

    }
}
