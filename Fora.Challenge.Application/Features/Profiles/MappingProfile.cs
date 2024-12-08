using AutoMapper;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Application.Models;
using Fora.Challenge.Application.Resolvers;
using Fora.Challenge.Domain.Entities;
using static Fora.Challenge.Application.Models.EdgarCompanyInfo;

namespace Fora.Challenge.Application.Features.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<EdgarCompanyInfo, Company>()
                .ForMember(dest => dest.Cik, opt => opt.MapFrom(src => src.Cik))
                .ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => src.EntityName))
                .ForMember(dest => dest.NetIncomeLossData, opt => opt.MapFrom<NetIncomeLossDataResolver>());

            CreateMap<InfoFactUsGaapIncomeLossUnitsUsd, NetIncomeLossData>();

            CreateMap<Company, GetCompanyDataResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EntityName))
                .ForMember(dest => dest.StandardFundableAmount, opt => opt.MapFrom<StandardFundableAmountResolver>())
                .ForMember(dest => dest.SpecialFundableAmount, opt => opt.MapFrom<SpecialFundableAmountResolver>());
        }
    }
}
