using Medirect.Application.Contracts;
using Medirect.Application.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Infrastructure.Services
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ICacheService _cacheService;
        private readonly ILogger<IExchangeRateHistoryRepository> _logger;

        public ExchangeRateRepository(IExchangeRateService exchangeRateService, ICacheService cacheService, ILogger<IExchangeRateHistoryRepository> logger)
        {
            _exchangeRateService = exchangeRateService;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<ExchangeRateDto?> GetExchangeRates(string baseCurrency, CancellationToken cancellationToken = default)
        {
            var rates = await _cacheService.TryGet<ExchangeRateDto>(baseCurrency);
            if (rates == null)
            {
                rates = await _exchangeRateService.GetExchangeRateAsync(baseCurrency, cancellationToken);
                if (rates == null)
                {
                    _logger.LogError("Could not get rates for base {baseCurrency}", baseCurrency);
                    return null;
                }

                await _cacheService.Set(baseCurrency, rates);
            }

            return rates;
        }
    }
}