using MediatR;
using Medirect.Application.Commands.GetExchangeRateCommand;
using Medirect.Test.API.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medirect.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExchangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("latest/{baseSymbol}/{symbol}")]
        public async Task<IActionResult> GetLatest(string baseSymbol, string symbol)
        {
            var result = await _mediator.Send(new GetExchangeRateCommandRequest { BaseCurrency = baseSymbol, ToCurrency = symbol },
                this.HttpContext.RequestAborted);
            return this.Respond(result);
        }
    }
}