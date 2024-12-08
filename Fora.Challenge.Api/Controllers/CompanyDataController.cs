using Fora.Challenge.Application.Features.FinancialData.Commands;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Challenge.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompanyDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="CompanyDataController"/> class.</summary>
        /// <param name="mediator">The mediator.</param>
        public CompanyDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Saves the company data.</summary>
        /// <param name="command">The command.</param>
        /// <returns>No content.</returns>
        [HttpPost("import")]
        public async Task<IActionResult> SaveCompanyData([FromBody] SaveCompanyDataCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>Gets the company data.</summary>
        /// <param name="firstLetter">The first letter.</param>
        /// <returns>The requested companies.</returns>
        [HttpGet()]
        public async Task<IActionResult> GetCompanyData([FromQuery]string? firstLetter)
        {
            var result = await _mediator.Send(new GetCompanyDataQuery() { FirstLetter = firstLetter});
            return Ok(result);
        }
    }
}
