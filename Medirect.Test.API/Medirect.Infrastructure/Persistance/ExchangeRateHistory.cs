using Medirect.Application.Contracts;
using Medirect.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Infrastructure.Persistance
{
    public class ExchangeRateHistoryRepository : GenericRepository<ExchangeRatesHistory>, IExchangeRateHistoryRepository
    {
        public ExchangeRateHistoryRepository(DataContext context) : base(context)
        {
        }
    }
}