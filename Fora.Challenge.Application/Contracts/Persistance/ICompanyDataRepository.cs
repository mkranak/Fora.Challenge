using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Contracts.Persistance
{
    public interface ICompanyDataRepository
    {
        Task<List<Company>> GetEdgarCompanyInfoAsync(string startsWith);
        Task SaveEdgarCompanyInfoAsync(IEnumerable<Company> companies);
    }
}
