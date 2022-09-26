using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace UpstreamWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _configuration;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    
    [HttpGet("downstream", Name = "GetWeatherForecast(Downstream)")]
    public async Task<string> GetDownstream()
    {
        var client = new HttpClient();
        var url = _configuration.GetSection("DownstreamUrl").Get<string>();
        var relativePath = _configuration.GetSection("DownstreamRelativePath").Get<string>() ?? string.Empty;
        
        _logger.LogInformation($"Making call to {url}/{relativePath}");
        var result = await client.GetAsync($"{url}/{relativePath}");
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadAsStringAsync();
        return response!;
    }
}
