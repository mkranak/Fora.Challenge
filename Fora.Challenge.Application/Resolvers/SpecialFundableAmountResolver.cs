using AutoMapper;
using Fora.Challenge.Application.Extensions;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Resolvers
{
    public class SpecialFundableAmountResolver : IValueResolver<Company, GetCompanyDataResponse, decimal>
    {
        public decimal Resolve(Company source, GetCompanyDataResponse destination, decimal destMember, ResolutionContext context)
        {
            var standardFundableAmount = destination.StandardFundableAmount;
            if (standardFundableAmount == 0)
                return 0;

            var specialFundableAmount = standardFundableAmount;
            var incomeLossData = source.NetIncomeLossData;

            // add 15% if the company name starts with a vowel
            if (source.EntityName.IsStartingWithVowel())
            {
                specialFundableAmount += standardFundableAmount * 0.15m; // 15 percent
            }

            // subtract 25% if 2022 income is less than 2021 income
            var bothYears = incomeLossData
                .Select(data => new
                {
                    Year = int.Parse(data.Frame.Substring(2)),
                    data.Val
                })
                .Where(data => data.Year == 2021 || data.Year == 2022)
                .ToList();

            var income2021 = bothYears.Find(data => data.Year == 2021)?.Val ?? 0;
            var income2022 = bothYears.Find(data => data.Year == 2022)?.Val ?? 0;

            if (income2022 < income2021)
            {
                specialFundableAmount -= standardFundableAmount * 0.25m;
            }
            
            // round standard fundable amount after calculations completed
            destination.StandardFundableAmount = 
                decimal.Round(standardFundableAmount, 2, MidpointRounding.AwayFromZero);

            return decimal.Round(specialFundableAmount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
