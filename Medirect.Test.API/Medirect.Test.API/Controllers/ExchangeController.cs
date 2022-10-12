using MediatR;
using Medirect.Application.Commands.CreateCurrencyConversionTradeComamnd;
using Medirect.Application.Commands.GetExchangeRateCommand;
using Medirect.Application.Model;
using Medirect.Test.API.Extension;
using Medirect.Test.API.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Medirect.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExchangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExchangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("latest/{baseSymbol}/{symbol}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResultWrapper<GetExchangeRateResponseModel>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ResultWrapper<GetExchangeRateResponseModel>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ResultWrapper<GetExchangeRateResponseModel>))]
        public async Task<IActionResult> GetLatest(string baseSymbol, string symbol)
        {
            var result = await _mediator.Send(new GetExchangeRateCommandRequest { BaseCurrency = baseSymbol, ToCurrency = symbol },
                this.HttpContext.RequestAborted);
            return this.Respond(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResultWrapper<CreateCurrencyConversionResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ResultWrapper<CreateCurrencyConversionResponse>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ResultWrapper<CreateCurrencyConversionResponse>))]
        [LimitRequests(MaxRequests = 10, TimeWindow = 60)]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyConversionRequest createCurrencyConversionRequest)
        {
            var result = await _mediator.Send(createCurrencyConversionRequest,
                this.HttpContext.RequestAborted);
            return this.Respond(result);
        }
    }
}