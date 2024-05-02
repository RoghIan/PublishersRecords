using PRMS.DTOs;
using PRMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IAppointedRepository
    {
        Task<IEnumerable<AppointedDto>> GetAppointedsAsync();
    }
}
