using FluentValidation;
using Mapster;
using MediatR;
using Medirect.Application.Contracts;
using Medirect.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application
{
    public static class TypesRegistration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            var assembly = typeof(TypesRegistration).Assembly;
            TypeAdapterConfig.GlobalSettings.Scan(assembly);

            return services
                .AddMediatR(assembly)
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddValidatorsFromAssembly(assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(HandleErrorBehavior<,>));
        }
    }
}