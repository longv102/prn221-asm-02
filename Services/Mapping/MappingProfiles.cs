using AutoMapper;
using BO.Dtos;
using Repositories;

namespace Services.Mapping
{
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Mapping entities and data transfer objects
        /// </summary>
        public MappingProfiles()
        {
            CreateMap<SystemAccount, SystemAccountDto>().ReverseMap();
            
            CreateMap<NewsArticle, NewsArticleDto>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.AccountName))
                .ForMember(x => x.TagNames, opt => opt.MapFrom(src => src.Tags.Select(t => t.TagName).ToList()))
                .ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
