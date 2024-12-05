using Fora.Challenge.Application.Models;

namespace Fora.Challenge.Application.Contracts.Infrastructure
{
    public interface IEdgarApiService
    {
        Task<EdgarCompanyInfo> FetchEdgarCompanyInfoAsync(int cik);
    }
}
