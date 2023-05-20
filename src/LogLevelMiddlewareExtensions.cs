using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog.Core;
using Serilog.Events;

namespace Serilog.Aspnetcore.Configuration.UI
{
  public static class LogLevelMiddlewareExtensions
  {
    /// <summary>
    /// Enable this package to be used in application and provide an UI to user for changing Log Level on a Html page in browser.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSerilogConfigurationUI(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<LogLevelMiddleware>();
    }

    /// <summary>
    /// Adds required object to be used by this package in application's dependency container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="loggingLevelSwitch">An object alredy instantiated to be set as dependency</param>
    /// <returns></returns>
    public static IServiceCollection AddSerilogLogLevelConfigurationUI(this IServiceCollection services, LoggingLevelSwitch loggingLevelSwitch)
    {
      services.AddSingleton(typeof(UIHandler));
      services.AddSingleton(typeof(LoggingLevelSwitch), loggingLevelSwitch);
      return services;
    }

    /// <summary>
    /// Set serilog to obey log level from loglevel switch and set current log level to be from appsetting.json or appsetting.<environment>.json configuration.
    /// If serilog configuration for Minimal Level's default is not defined then it will set "Error" to be default log level for whole application.
    /// </summary>
    /// <param name="loggingConfiguration"></param>
    /// <param name="loggingLevelSwitch"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static LoggerConfiguration AddLogLevelManager(this LoggerConfiguration loggingConfiguration, LoggingLevelSwitch loggingLevelSwitch, IConfiguration configuration)
    {
      var logLevelString = configuration.GetValue<string>("Serilog:MinimumLevel:Default");
      loggingLevelSwitch.MinimumLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), string.IsNullOrWhiteSpace(logLevelString) ? "Error" : logLevelString, true);
      loggingConfiguration.MinimumLevel.ControlledBy(loggingLevelSwitch);
      return loggingConfiguration;
    }
  }
}