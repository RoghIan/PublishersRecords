using AutoMapper;
using PRMS.DTOs;
using PRMS.Entities;
using PRMS.Extensions;

namespace PRMS.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Publisher, PublisherDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.BirthDate.CalculateAge()));
            CreateMap<Group, GroupDto>();
            CreateMap<Appointed, AppointedDto>();
        }
    }
}
