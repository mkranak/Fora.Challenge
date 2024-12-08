using Fora.Challenge.Application.Models;

namespace Fora.Challenge.Application.Contracts.Infrastructure
{
    public interface IEdgarApiService
    {
        /// <summary>Fetches the edgar company information asynchronous.</summary>
        /// <param name="cik">The cik.</param>
        /// <returns>Company Info.</returns>
        Task<EdgarCompanyInfo> FetchEdgarCompanyInfoAsync(int cik);
    }
}
