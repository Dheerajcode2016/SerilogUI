using Serilog;
using Serilog.Aspnetcore.Configuration.UI;
using Serilog.Core;

namespace TestAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      var applicationEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      // Add services to the container.
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsetting.json")
        .AddJsonFile($"appsetting.{applicationEnvironment.ToLower()}.json")
        .AddEnvironmentVariables()
        .Build();

      var logLevelSwitch = new LoggingLevelSwitch();
      logLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Fatal;

      builder.Logging.ClearProviders();
      builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
      {
        loggerConfiguration.ReadFrom.Configuration(config);
        loggerConfiguration.AddLogLevelManager(logLevelSwitch, config);
      });

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddSerilogLogLevelConfigurationUI(logLevelSwitch);
      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }
      app.UseSerilogConfigurationUI();
      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}