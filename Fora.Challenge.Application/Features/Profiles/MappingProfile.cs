using AutoMapper;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Application.Models;
using Fora.Challenge.Application.Resolvers;
using Fora.Challenge.Domain.Entities;
using System.Text.RegularExpressions;

namespace Fora.Challenge.Application.Features.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<EdgarCompanyInfo, Company>()
                .ForMember(dest => dest.Cik, opt => opt.MapFrom(src => src.Cik))
                .ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => src.EntityName))
                .ForMember(dest => dest.NetIncomeLossData, opt => opt.MapFrom(src =>
                    src.Facts.UsGaap.NetIncomeLoss.Units.Usd
                        .Where(u => 
                            u.Form == "10-K" && 
                            u.Frame != null && 
                            Regex.IsMatch(u.Frame, @"^CY\d{4}$"))));

            CreateMap<EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd, NetIncomeLossData>()
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => src.Form))
                .ForMember(dest => dest.Frame, opt => opt.MapFrom(src => src.Frame))
                .ForMember(dest => dest.Val, opt => opt.MapFrom(src => src.Val));

            CreateMap<Company, GetCompanyDataResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EntityName))
                .ForMember(dest => dest.StandardFundableAmount, opt => opt.MapFrom<StandardFundableAmountResolver>())
                .ForMember(dest => dest.SpecialFundableAmount, opt => opt.MapFrom<SpecialFundableAmountResolver>());
        }
    }
}
