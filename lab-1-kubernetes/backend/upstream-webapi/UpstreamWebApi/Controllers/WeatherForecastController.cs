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

    [HttpGet(Name = "GetWeatherForecast(Downstream)")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var client = new HttpClient();
        var url = _configuration.GetSection("DownstreamUrl").Get<string>();
        _logger.LogInformation($"Making call to {url}/weatherforcast");
        var result = await client.GetAsync($"{url}/weatherforecast");
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadAsStringAsync();
        _logger.LogInformation("Deserializing!");
        var forecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(response, 
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        return forecasts!;
    }
}
