using MediatR;
using Medirect.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Commands.GetExchangeRateCommand
{
    public class GetExchangeRateCommandRequest : IRequest<ResultWrapper<GetExchangeRateResponseModel>>
    {
        public string BaseCurrency { get; set; }
        public string ToCurrency { get; set; }
    }
}