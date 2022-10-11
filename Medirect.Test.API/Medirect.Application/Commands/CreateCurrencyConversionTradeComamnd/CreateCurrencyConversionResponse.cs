using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Commands.CreateCurrencyConversionTradeComamnd
{
    public class CreateCurrencyConversionResponse
    {
        public string BaseCurrency { get; set; }
        public decimal BaseAmount { get; set; }
        public string ToCurrency { get; set; }
        public decimal ToAmount { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}