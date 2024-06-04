using AutoMapper;
using BO.Dtos;
using Repositories;

namespace Services.Mapping
{
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MappingProfiles()
        {
            CreateMap<SystemAccount, SystemAccountDto>().ReverseMap();  
        }
    }
}
