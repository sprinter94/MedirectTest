using MediatR;
using Medirect.Application.Contracts;
using Medirect.Application.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Commands.GetExchangeRateCommand
{
    public class GetExchangeRateCommandRequestHandler : IRequestHandler<GetExchangeRateCommandRequest, ResultWrapper<GetExchangeRateResponseModel>>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly ILogger<GetExchangeRateCommandRequestHandler> _ilogger;

        public GetExchangeRateCommandRequestHandler(IExchangeRateRepository exchangeRateRepository,
            ILogger<GetExchangeRateCommandRequestHandler> ilogger)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _ilogger = ilogger;
        }

        public async Task<ResultWrapper<GetExchangeRateResponseModel>> Handle(GetExchangeRateCommandRequest request, CancellationToken cancellationToken)
        {
            var rates = await _exchangeRateRepository.GetExchangeRates(request.BaseCurrency);
            if (rates == null)
            {
                return ResultWrapper<GetExchangeRateResponseModel>.Internal("could not retrieve rates.");
            }

            var exchangeRate = rates.Rates.GetType().GetProperty(request.ToCurrency.ToUpper()).GetValue(rates.Rates, null);

            if (exchangeRate == null)
            {
                _ilogger.LogError("Could not get value from base {baseCurrency}", request.BaseCurrency);
                return ResultWrapper<GetExchangeRateResponseModel>.Internal("could not retrieve rates.");
            }
            return ResultWrapper<GetExchangeRateResponseModel>.Ok(new GetExchangeRateResponseModel { ExchangeRate = Convert.ToDouble(exchangeRate.ToString()) });
        }
    }
}