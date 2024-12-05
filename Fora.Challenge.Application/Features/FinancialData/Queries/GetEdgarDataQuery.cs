using Fora.Challenge.Application.Models;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetEdgarDataQuery : IRequest<EdgarCompanyInfo>
    {
        public int Cik { get; set; }
    }
}
