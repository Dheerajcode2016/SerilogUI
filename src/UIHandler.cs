/// Reference : https://nblumhardt.com/2014/10/dynamically-changing-the-serilog-level/

using Microsoft.AspNetCore.Http;

using Serilog.Core;
using Serilog.Events;

using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Serilog.Aspnetcore.Configuration.UI
{
  public class UIHandler
  {
    private LoggingLevelSwitch LogLevelSwitch { get; }

    public UIHandler(LoggingLevelSwitch logLevelSwitch)
    {
      LogLevelSwitch = logLevelSwitch;
    }

    public async Task<HttpResponse> GetLogUI(HttpContext httpContext)
    {
      var response = httpContext.Response;

      var htmlContent = await ReadStreamToStringAsync(Assembly.GetExecutingAssembly().GetManifestResourceStream("Serilog.Aspnetcore.Configuration.UI.index.htm"));
      htmlContent = htmlContent.Replace("{LogLevel}", LogLevelSwitch.MinimumLevel.ToString());
      if (!string.IsNullOrWhiteSpace(htmlContent))
      {
        response.ContentType = "text/html";
        using (var buffer = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
        {
          buffer.Position = 0;
          await buffer?.CopyToAsync(response.Body);
          
        }
      }
      return response;
    }

    public async Task<HttpResponse> PostUpdateLogLevel(HttpContext httpContext)
    {
      var request = httpContext.Request;
      var requestValue = await ReadStreamToStringAsync(httpContext.Request.Body);
      if (!String.IsNullOrWhiteSpace(requestValue))
      {
        Match matchResult = Regex.Match(requestValue, "^(?i)(LogLevelOptions=)([0-6]+)");
        if (matchResult.Success)
        {
          LogLevelSwitch.MinimumLevel = (LogEventLevel)Convert.ToInt32(matchResult.Groups[2].Value);
        }
      }
      return await GetLogUI(httpContext);
    }

    private async Task<string> ReadStreamToStringAsync(Stream stream)
    {
      var requestText = string.Empty;
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        requestText = await reader.ReadToEndAsync();
      }

      return requestText;
    }
  }
}