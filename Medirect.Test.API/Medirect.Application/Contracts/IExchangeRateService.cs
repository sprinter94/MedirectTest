using Medirect.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Contracts
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateDto> GetExchangeRateAsync(string baseCurrency, CancellationToken cancellationToken = default);
    }
}