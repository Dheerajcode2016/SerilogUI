using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      _logger.LogInformation("Environment : " + Environment.GetEnvironmentVariable("Dheeraj"));
      _logger.LogDebug("Environment : " + Environment.GetEnvironmentVariable("Dheeraj"));
      _logger.LogError("Environment : " + Environment.GetEnvironmentVariable("Dheeraj"));
      _logger.LogCritical("Environment : " + Environment.GetEnvironmentVariable("Dheeraj"));
      Console.WriteLine();
      Console.WriteLine("Environment : " + Environment.GetEnvironmentVariable("Dheeraj"));
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
  }
}