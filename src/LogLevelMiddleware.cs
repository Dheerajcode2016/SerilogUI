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
        if (httpContext.Request.Method == HttpMethod.Get.ToString() &&
           httpContext.Request.Path.HasValue &&
           httpContext.Request.Path.Value.Contains("/log/UI", StringComparison.CurrentCultureIgnoreCase))
        {
          await LogUIHandler.GetLogUI(httpContext);
        }
        if (httpContext.Request.Method == HttpMethod.Post.ToString() &&
           httpContext.Request.Path.HasValue &&
           httpContext.Request.Path.Value.Contains("/log/UI", StringComparison.CurrentCultureIgnoreCase))
        {
          await LogUIHandler.PostUpdateLogLevel(httpContext);
        }
      }
    }
  }
}