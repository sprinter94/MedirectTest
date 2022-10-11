using Medirect.Application;
using Medirect.Application.Settings;
using Medirect.Infrastructure;
using Medirect.Infrastructure.Persistance;
using Medirect.Test.API;
using Medirect.Test.API.Helper;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(Configuration);
builder.Services.Configure<GenSettings>(Configuration.GetSection("GenSettings"));
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), serverVersion));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(Configuration);
builder.Services.AddAuthorization();
builder.Services.Configure<JWT>(Configuration.GetSection("Jwt"));

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();