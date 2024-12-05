using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Contracts.Persistance
{
    public interface ICompanyDataRepository
    {
        Task<Company> GetEdgarCompanyInfoAsync(int cik);
        Task SaveEdgarCompanyInfoAsync(IEnumerable<Company> companies);
    }
}
