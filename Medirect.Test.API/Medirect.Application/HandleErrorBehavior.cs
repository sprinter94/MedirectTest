using Mapster;
using MediatR;
using Medirect.Application.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application
{
    public class HandleErrorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<HandleErrorBehavior<TRequest, TResponse>> _logger;

        public HandleErrorBehavior(ILogger<HandleErrorBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An internal error occurred");
                return ResultWrapper.Internal("Internal error").Adapt<TResponse>();
            }
        }
    }
}