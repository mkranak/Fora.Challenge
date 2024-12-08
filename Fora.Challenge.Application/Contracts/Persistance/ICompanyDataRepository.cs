using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Contracts.Persistance
{
    public interface ICompanyDataRepository
    {
        /// <summary>Gets the company data asynchronous.</summary>
        /// <param name="firstLetter">First letter filter.</param>
        /// <returns>Company Data.</returns>
        Task<List<Company>> GetCompanyDataAsync(string firstLetter);

        /// <summary>Saves the company data asynchronous.</summary>
        /// <param name="companies">The companies.</param>
        /// <returns>A task.</returns>
        Task SaveCompanyDataAsync(IEnumerable<Company> companies);
    }
}
