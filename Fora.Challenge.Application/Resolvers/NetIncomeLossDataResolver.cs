using AutoMapper;
using Fora.Challenge.Application.Models;
using Fora.Challenge.Domain.Entities;
using System.Text.RegularExpressions;

namespace Fora.Challenge.Application.Resolvers
{
    public class NetIncomeLossDataResolver : IValueResolver<EdgarCompanyInfo, Company, ICollection<NetIncomeLossData>>
    {
        /// <summary>Filter only valid NetIncomeLossData for company.</summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object, if exists</param>
        /// <param name="destMember">Destination member</param>
        /// <param name="context">The context of the mapping</param>
        /// <returns>A collection of NetIncomeLossData for a company.</returns>
        public ICollection<NetIncomeLossData> Resolve(EdgarCompanyInfo source, Company destination, ICollection<NetIncomeLossData> destMember, ResolutionContext context)
        {
            if (source?.Facts?.UsGaap?.NetIncomeLoss?.Units?.Usd == null)
                return [];

            return source.Facts.UsGaap.NetIncomeLoss.Units.Usd
                .Where(u => u.Form == "10-K" && u.Frame != null && Regex.IsMatch(u.Frame, @"^CY\d{4}$"))
                .Select(u => new NetIncomeLossData
                {
                    Form = u.Form,
                    Frame = u.Frame,
                    Val = u.Val
                })
                .ToList();
        }
    }
}
