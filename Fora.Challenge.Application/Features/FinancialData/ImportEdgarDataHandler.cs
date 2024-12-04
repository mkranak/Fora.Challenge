using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Models;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData
{
    public class ImportEdgarDataHandler : IRequestHandler<ImportEdgarDataCommand, EdgarCompanyInfo>
    {
        private readonly IEdgarApiService _edgarApiService;

        public ImportEdgarDataHandler(IEdgarApiService edgarApiService)
        {
            _edgarApiService = edgarApiService;
        }

        public async Task<EdgarCompanyInfo> Handle(ImportEdgarDataCommand request, CancellationToken cancellationToken)
        {
            return await _edgarApiService.FetchEdgarCompanyInfoAsync(request.Cik);
        }
    }
}
