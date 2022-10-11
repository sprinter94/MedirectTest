using MediatR;
using Medirect.Application.Contracts;
using Medirect.Application.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Commands.CreateCurrencyConversionTradeComamnd
{
    public class CreateCurrencyConversionRequestHandler : IRequestHandler<CreateCurrencyConversionRequest, ResultWrapper<CreateCurrencyConversionResponse>>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly IExchangeRateHistoryRepository _exchangeRateHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<CreateCurrencyConversionRequestHandler> _logger;

        public CreateCurrencyConversionRequestHandler(IExchangeRateRepository exchangeRateRepository,
            IExchangeRateHistoryRepository exchangeRateHistoryRepository, IUserRepository userRepository, ICurrentUserService currentUserService,
            ILogger<CreateCurrencyConversionRequestHandler> logger)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _exchangeRateHistoryRepository = exchangeRateHistoryRepository;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ResultWrapper<CreateCurrencyConversionResponse>> Handle(CreateCurrencyConversionRequest request, CancellationToken cancellationToken)
        {
            var rates = await _exchangeRateRepository.GetExchangeRates(request.BaseCurrency);
            if (rates == null)
            {
                return ResultWrapper<CreateCurrencyConversionResponse>.Internal("could not retrieve rates.");
            }

            var exchangeRate = rates.Rates.GetType().GetProperty(request.ToCurrency.ToUpper()).GetValue(rates.Rates, null);

            var toAmount = request.BaseAmount * Convert.ToDecimal(exchangeRate);
            var username = _currentUserService.GetUsername();
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogError("No user was found in httpcontext");
                return ResultWrapper<CreateCurrencyConversionResponse>.Bad("No user was found.");
            }

            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogError("Could not retrieve user information {user}", username);
                return ResultWrapper<CreateCurrencyConversionResponse>.Internal("could not find user.");
            }

            _exchangeRateHistoryRepository.Add(new Domain.ExchangeRatesHistory
            {
                BaseAmount = request.BaseAmount,
                BaseCurrency = request.BaseCurrency,
                DateInserted = DateTime.Now,
                ExchangeRate = Convert.ToDecimal(exchangeRate),
                ToAmount = toAmount,
                ToCurrency = request.ToCurrency,
                UserId = user.Id
            });

            await _exchangeRateHistoryRepository.SaveChangesAsync();

            return ResultWrapper<CreateCurrencyConversionResponse>.Created(new CreateCurrencyConversionResponse
            {
                BaseAmount = request.BaseAmount,
                BaseCurrency = request.BaseCurrency.ToUpper(),
                ExchangeRate = Convert.ToDecimal(exchangeRate),
                ToAmount = toAmount,
                ToCurrency = request.ToCurrency.ToUpper(),
            });
        }
    }
}