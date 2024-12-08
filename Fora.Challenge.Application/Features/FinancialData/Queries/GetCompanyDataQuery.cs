using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataQuery : IRequest<List<GetCompanyDataResponse>>
    {

        /// <summary>Gets or sets the filter (for first letter).</summary>
        /// <value>The filter (for first letter).</value>
        public string? Filter { get; set; }
    }
}
