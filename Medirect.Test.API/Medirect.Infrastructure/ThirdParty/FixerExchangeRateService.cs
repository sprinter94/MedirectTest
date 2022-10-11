using Medirect.Application.Contracts;
using Medirect.Application.Model;
using Medirect.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Infrastructure.ThirdParty
{
    internal class FixerExchangeRateService : BaseClient, IExchangeRateService
    {
        public FixerExchangeRateService(HttpClient httpClient, ILogger<FixerExchangeRateService> logger, IOptions<GenSettings> options) : base(httpClient, logger)
        {
            SetApiKey(options.Value.APISettings.FixerKey);
        }

        public async Task<ExchangeRateDto> GetExchangeRateAsync(string baseCurrency, CancellationToken cancellationToken = default)
        {
            var uri = $"https://api.apilayer.com/fixer/latest?base={baseCurrency}";
            return await SendRequest<string, ExchangeRateDto>(string.Empty, uri, VerbType.GET, cancellationToken);
        }
    }
}