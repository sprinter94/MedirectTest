using Medirect.Application.Contracts;
using Medirect.Test.API.Helper;
using System.Net;

namespace Medirect.Test.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService,
            IExchangeRateHistoryRepository exchangeRateHistoryRepository, IUserRepository userRepository)
        {
            var endpoint = context.GetEndpoint();
            var rateLimitRequest = endpoint?.Metadata.GetMetadata<LimitRequests>();
            if (rateLimitRequest is null)
            {
                await _next(context);
                return;
            }

            var username = currentUserService.GetUsername();

            if (string.IsNullOrEmpty(username))
            {
                await _next(context);
                return;
            }

            var user = await userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                await _next(context);
                return;
            }
            var exchangehistory = (await exchangeRateHistoryRepository.FindByConditionAsync(x => x.UserId == user.Id
            && DateTime.Now < x.DateInserted.AddMinutes(rateLimitRequest.TimeWindow))).OrderByDescending(x => x.Id).ToList();
            if (exchangehistory == null)
            {
                await _next(context);
                return;
            }

            if (exchangehistory.Count >= rateLimitRequest.MaxRequests)
            {
                var diff = exchangehistory.First().DateInserted.AddMinutes(rateLimitRequest.TimeWindow).Subtract(DateTime.Now);
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.Headers.Add("retryAfter", $"{diff.TotalSeconds.ToString()}s");
                return;
            }

            await _next(context);
        }
    }
}