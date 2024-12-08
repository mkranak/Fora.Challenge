using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataQuery : IRequest<List<GetCompanyDataResponse>>
    {
        /// <summary>Gets or sets the first letter filter.</summary>
        /// <value>The first letter for the filter.</value>
        public string? FirstLetter { get; set; }
    }
}
