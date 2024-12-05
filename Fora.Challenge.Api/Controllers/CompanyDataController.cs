using Fora.Challenge.Application.Exceptions;
using Fora.Challenge.Application.Features.FinancialData.Commands;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("import")]
        public async Task<IActionResult> SaveCompanyData([FromBody] SaveCompanyDataCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet()]
        public async Task<IActionResult> GetCompanyData([FromQuery]string? startsWith)
        {
            // Validate here with validator and validationException
            if (startsWith != null && startsWith.Length > 1)
                throw new Exception("The filter starts with can only be one character.");

            var result = await _mediator.Send(new GetCompanyDataQuery() { StartsWith = startsWith});
            if (result == null)
                return NotFound($"Data not found or failed to fetch.");

            return Ok(result);
        }
    }
}
