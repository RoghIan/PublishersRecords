using PRMS.DTOs;
using PRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Interfaces
{
    public interface IPublihserRepository
    {
        void Update(Publisher publisher);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<PublisherDto>> GetPublishersAsync();
        Task<PublisherDto> GetPublisherByIdAsync(int id);
    }
}
