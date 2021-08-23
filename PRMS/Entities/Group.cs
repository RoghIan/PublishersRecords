using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRMS.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OverseerPublisherId { get; set; }
        public int AssistantPublisherId { get; set; }
    }
}
