using AutoMapper;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Resolvers
{
    public class SpecialFundableAmountResolver : IValueResolver<Company, GetCompanyDataResponse, decimal>
    {
        public decimal Resolve(Company source, GetCompanyDataResponse destination, decimal destMember, ResolutionContext context)
        {
            var standardFundableAmount = new StandardFundableAmountResolver().Resolve(source, destination, destMember, context);
            var incomeLossData = source.NetIncomeLossData;

            var specialFundableAmount = standardFundableAmount;

            // Add 15% if the company name starts with a vowel
            if (!string.IsNullOrEmpty(source.EntityName) && "AEIOUaeiou".Contains(source.EntityName[0]))
            {
                specialFundableAmount += standardFundableAmount * 0.15m;
            }

            // Subtract 25% if 2022 income is less than 2021 income
            var relevantYears = incomeLossData
                .Where(data => data.Frame.StartsWith("CY"))
                .Select(data => new
                {
                    Year = int.Parse(data.Frame.Substring(2)),
                    data.Val
                })
                .Where(data => data.Year >= 2018 && data.Year <= 2022)
                .ToList();

            var income2021 = relevantYears.FirstOrDefault(data => data.Year == 2021)?.Val ?? 0;
            var income2022 = relevantYears.FirstOrDefault(data => data.Year == 2022)?.Val ?? 0;

            if (income2022 < income2021)
            {
                specialFundableAmount -= standardFundableAmount * 0.25m;
            }

            return specialFundableAmount;
        }
    }
}
