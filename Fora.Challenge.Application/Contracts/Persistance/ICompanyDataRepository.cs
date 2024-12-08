using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Contracts.Persistance
{
    public interface ICompanyDataRepository
    {
        /// <summary>Gets the company data asynchronous.</summary>
        /// <param name="filter">The filter (for the first letter).</param>
        /// <returns>The requested company data..</returns>
        Task<List<Company>> GetCompanyDataAsync(string filter);

        /// <summary>Saves the company data asynchronous.</summary>
        /// <param name="companies">The companies.</param>
        /// <returns>A task.</returns>
        Task SaveCompanyDataAsync(IEnumerable<Company> companies);
    }
}
