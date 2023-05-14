using Microsoft.AspNetCore.Mvc;

using Serilog.Core;

namespace TestAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LogController : ControllerBase
  {
    private LoggingLevelSwitch _logLevelSwitch;

    public LogController(LoggingLevelSwitch logLevelSwitch)
    {
      _logLevelSwitch = logLevelSwitch;
    }

    [HttpGet]
    [Route("/{level}")]
    public void ChangeLogLevel([FromRoute] LogLevel level)
    {
      _logLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
    }
  }
}