using AutoMapper;
using Entities.Dto;
using Entities.Models;
using Claim = Entities.Models.Claim;

namespace CompanyClaimsApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.HasActivePolicy, opt => opt.Ignore());

            CreateMap<Claim, ClaimDto>()
                .ForMember(dest => dest.ClaimAgeInDays, opt => opt.Ignore());

            CreateMap<ClaimDto, Claim>();
        }
    }
}
