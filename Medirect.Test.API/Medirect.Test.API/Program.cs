using Medirect.Application;
using Medirect.Application.Settings;
using Medirect.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(Configuration);
builder.Services.Configure<GenSettings>(Configuration.GetSection("GenSettings"));

var Log = new LoggerConfiguration()
.ReadFrom.Configuration(Configuration)
.WriteTo.File(@"G:\Documents\brandon\Test2\Medirect.Test.API")
.CreateLogger();

var loggerFactory = new LoggerFactory().AddSerilog(Log);
builder.Services.AddSingleton(loggerFactory);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();