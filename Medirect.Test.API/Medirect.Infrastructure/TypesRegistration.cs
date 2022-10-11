using Medirect.Application.Contracts;
using Medirect.Infrastructure.Persistance;
using Medirect.Infrastructure.Services;
using Medirect.Infrastructure.ThirdParty;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Infrastructure
{
    public static class TypesRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IExchangeRateService, FixerExchangeRateService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExchangeRateHistoryRepository, ExchangeRateHistoryRepository>();
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services
                .AddScoped<ICacheService, RedisCacheService>()
            .AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "ExchangeRates_";
            });
            return services;
        }
    }
}