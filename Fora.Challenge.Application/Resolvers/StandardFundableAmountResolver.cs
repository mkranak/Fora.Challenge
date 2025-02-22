﻿using AutoMapper;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Resolvers
{
    public class StandardFundableAmountResolver : IValueResolver<Company, GetCompanyDataResponse, decimal>
    {
        /// <summary>Calculates the standard fundable amount.</summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object, if exists</param>
        /// <param name="destMember">Destination member</param>
        /// <param name="context">The context of the mapping</param>
        /// <returns>The standard fundable amount.</returns>
        public decimal Resolve(Company source, GetCompanyDataResponse destination, decimal destMember, ResolutionContext context)
        {
            var incomeLossData = source.NetIncomeLossData;

            // filter data to include only the relevant years
            var relevantYears = incomeLossData
                .Select(data => new
                {
                    Year = int.Parse(data.Frame.Substring(2)),
                    data.Val
                })
                .Where(data => data.Year >= 2018 && data.Year <= 2022)
                .ToList();

            // validate if all years exist
            if (relevantYears.Select(data => data.Year).Distinct().Count() < 5)
                return 0;

            // validate positive income in 2021 and 2022
            var income2021 = relevantYears.Find(data => data.Year == 2021)?.Val ?? 0;
            var income2022 = relevantYears.Find(data => data.Year == 2022)?.Val ?? 0;

            if (income2021 <= 0 || income2022 <= 0)
                return 0;

            // get fundable amount from highest income
            var highestIncome = relevantYears.Max(data => data.Val);

            return highestIncome >= 10000000000 // 10 billion
                ? highestIncome * 0.1233m
                : highestIncome * 0.2151m;
        }
    }
}
