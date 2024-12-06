using AutoMapper;
using Fora.Challenge.Application.Models;
using Fora.Challenge.Domain.Entities;
using System.Text.RegularExpressions;


namespace Fora.Challenge.Application.Resolvers
{
    public class NetIncomeLossDataResolver : IValueResolver<EdgarCompanyInfo, Company, ICollection<NetIncomeLossData>>
        {
        public ICollection<NetIncomeLossData> Resolve(EdgarCompanyInfo source, Company destination, ICollection<NetIncomeLossData> destMember, ResolutionContext context)
        {
            if (source.Facts?.UsGaap?.NetIncomeLoss?.Units?.Usd == null)
                return new List<NetIncomeLossData>();

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
