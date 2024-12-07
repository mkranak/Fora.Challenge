using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Contracts.Persistance
{
    public interface ICompanyDataRepository
    {
        Task<List<Company>> GetCompanyDataAsync(string startsWith);
        Task SaveCompanyDataAsync(IEnumerable<Company> companies);
    }
}
