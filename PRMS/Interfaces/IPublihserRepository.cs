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
        void Add(Publisher publisher);
        void Delete(int id);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<PublisherDto>> GetPublishersDtoAsync();
        Task<PublisherDto> GetPublisherDtoByIdAsync(int id);
        Task<IEnumerable<Publisher>> GetPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
    }
}
