using AutoMapper;
using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Models.DTOs;

namespace PersonalProfileAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Education, EducationDTO>().ReverseMap();
            CreateMap<Education, AddEducationDTO>().ReverseMap();
            CreateMap<Education, UpdateEducationDTO>().ReverseMap();
        }
    }
}
