using AutoMapper;
using Movies.Core.Entities;
using Movies.Core.Models;

namespace Movies.Core.Profiles
{
    public class ApiKeyProfile : Profile
    {
        public ApiKeyProfile()
        {
            CreateMap<ApiKey, ApiKeyDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id));
        }
    }
}
