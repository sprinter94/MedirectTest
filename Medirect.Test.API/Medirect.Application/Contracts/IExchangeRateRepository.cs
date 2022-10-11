using Medirect.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Contracts
{
    public interface IExchangeRateRepository
    {
        Task<ExchangeRateDto?> GetExchangeRates(string baseCurency, CancellationToken cancellationToken = default);
    }
}