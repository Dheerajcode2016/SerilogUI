using Microsoft.AspNetCore.Http;

namespace Serilog.Aspnetcore.Configuration.UI
{
  public class LogLevelMiddleware
  {
    private readonly RequestDelegate _next;
    private UIHandler LogUIHandler;

    public LogLevelMiddleware(RequestDelegate next, UIHandler logUiHandler)
    {
      _next = next;
      LogUIHandler = logUiHandler;
    }

    public async Task Invoke(HttpContext httpContext)
    {
      if (!httpContext.Request.Path.Value.Contains("/log/UI", StringComparison.CurrentCultureIgnoreCase))
      {
        await _next.Invoke(httpContext);
      }
      else
      {
        switch (httpContext.Request.Method)
        {
          case "GET":
            await LogUIHandler.GetLogUI(httpContext);
            break;

          case "POST":
            await LogUIHandler.PostUpdateLogLevel(httpContext);
            break;

          default:
            await _next.Invoke(httpContext);
            break;
        }
      }
    }
  }
}