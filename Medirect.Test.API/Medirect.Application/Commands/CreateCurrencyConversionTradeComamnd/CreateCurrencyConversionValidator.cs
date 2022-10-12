using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Commands.CreateCurrencyConversionTradeComamnd
{
    public class CreateCurrencyConversionValidator : AbstractValidator<CreateCurrencyConversionRequest>
    {
        public CreateCurrencyConversionValidator()
        {
            RuleFor(x => x.BaseCurrency).MaximumLength(3).MinimumLength(3).NotEmpty().NotNull();
            RuleFor(x => x.ToCurrency).NotEmpty().NotNull().MaximumLength(3).MinimumLength(3);
            RuleFor(x => x.BaseAmount).NotEmpty();
        }
    }
}