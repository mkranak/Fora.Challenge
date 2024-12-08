using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataQuery : IRequest<List<GetCompanyDataResponse>>
    {
        public string? FirstLetter { get; set; }
    }
}
