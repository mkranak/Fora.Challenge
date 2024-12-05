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

        [HttpGet("{cik}")]
        public async Task<IActionResult> GetCompanyData(int cik)
        {
            var result = await _mediator.Send(new GetEdgarDataQuery { Cik = cik });

            if (result == null)
            {
                return NotFound($"Data for CIK {cik} not found or failed to fetch.");
            }

            return Ok(result);
        }
    }
}
