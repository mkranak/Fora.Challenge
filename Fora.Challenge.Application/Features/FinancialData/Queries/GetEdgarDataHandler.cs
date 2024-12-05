using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Models;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetEdgarDataHandler : IRequestHandler<GetEdgarDataQuery, EdgarCompanyInfo>
    {
        private readonly IEdgarApiService _edgarApiService;

        public GetEdgarDataHandler(IEdgarApiService edgarApiService)
        {
            _edgarApiService = edgarApiService;
        }

        public async Task<EdgarCompanyInfo> Handle(GetEdgarDataQuery request, CancellationToken cancellationToken)
        {
            return await _edgarApiService.FetchEdgarCompanyInfoAsync(request.Cik);
        }
    }
}
