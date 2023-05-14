using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Serilog.Aspnetcore.Configuration.UI
{
  public static class LogLevelMiddlewareExtensions
  {
    public static IApplicationBuilder UseSerlogConfigurationUI(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<LogLevelMiddleware>();
    }

    public static IServiceCollection AddSerlogConfigurationUI(this IServiceCollection services)
    {
      services.AddSingleton(typeof(UIHandler));
      return services;
    }
  }
}