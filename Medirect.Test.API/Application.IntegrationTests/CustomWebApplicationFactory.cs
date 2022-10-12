using Medirect.Application.Contracts;
using Medirect.Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;

namespace Application.IntegrationTests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices((builder, services) =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

                services
                    .Remove<ICurrentUserService>()
                    .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                        s.GetUsername() == "joe.borg"));

                services
                    .Remove<DbContextOptions<DataContext>>()
                    .AddDbContext<DataContext>((sp, options) =>
                        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion,
                            builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
            });
        }
    }
}