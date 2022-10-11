using FluentValidation;
using Mapster;
using MediatR;
using Medirect.Application.Contracts;
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
                .AddValidatorsFromAssembly(assembly);
        }
    }
}