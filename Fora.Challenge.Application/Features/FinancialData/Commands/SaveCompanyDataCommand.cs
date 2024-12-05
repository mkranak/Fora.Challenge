using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Commands
{
    public class SaveCompanyDataCommand : IRequest
    {
        /// <summary>Gets or sets the ciks.</summary>
        public List<int> Ciks { get; set; }
    }
}
