using Fora.Challenge.Application.Features.FinancialData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ImportCompanyData(string cik)
        {
            var result = await _mediator.Send(new ImportEdgarDataCommand { Cik = cik });

            if (result == null)
            {
                return NotFound($"Data for CIK {cik} not found or failed to fetch.");
            }

            return Ok(result);
        }
    }
}
