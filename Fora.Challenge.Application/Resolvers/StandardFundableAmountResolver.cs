using AutoMapper;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Resolvers
{
    public class StandardFundableAmountResolver : IValueResolver<Company, GetCompanyDataResponse, decimal>
    {
        public decimal Resolve(Company source, GetCompanyDataResponse destination, decimal destMember, ResolutionContext context)
        {
            var incomeLossData = source.NetIncomeLossData;

            // Filter income data for years 2018 to 2022
            var relevantYears = incomeLossData
                .Select(data => new
                {
                    Year = int.Parse(data.Frame.Substring(2)),
                    data.Val
                })
                .Where(data => data.Year >= 2018 && data.Year <= 2022)
                .ToList();

            // Ensure all years from 2018 to 2022 are present
            if (relevantYears.Select(data => data.Year).Distinct().Count() < 5)
                return 0;

            // Ensure positive income in 2021 and 2022
            var income2021 = relevantYears.FirstOrDefault(data => data.Year == 2021)?.Val ?? 0;
            var income2022 = relevantYears.FirstOrDefault(data => data.Year == 2022)?.Val ?? 0;

            if (income2021 <= 0 || income2022 <= 0)
                return 0;

            // Calculate fundable amount based on highest income
            var highestIncome = relevantYears.Max(data => data.Val);
            return highestIncome >= 10_000_000_000
                ? highestIncome * 0.1233m
                : highestIncome * 0.2151m;
        }
    }
}
