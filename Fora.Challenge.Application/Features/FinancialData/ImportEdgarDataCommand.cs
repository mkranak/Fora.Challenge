using Fora.Challenge.Application.Models;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData
{
    public class ImportEdgarDataCommand : IRequest<EdgarCompanyInfo>
    {
        public string Cik { get; set; }
    }
}
