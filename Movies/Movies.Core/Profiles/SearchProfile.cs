using AutoMapper;
using Movies.Core.Entities;
using Movies.Core.Models;

namespace Movies.Core.Profiles
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<Search, SearchDto>();
            CreateMap<SearchReport, SearchReportDto>();
        }
    }
}
