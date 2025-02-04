using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] _summaries = new[]
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
        => GetWeatherForcasts();

    [HttpGet("GenericClassActionDirect")]
    public ActionResult<IEnumerable<WeatherForecast>> GenericClassActionDirect()
        => GetWeatherForcasts();

    [HttpGet("GenericClassAction")]
    public ActionResult<IEnumerable<WeatherForecast>> GenericClassActionOk()
        => Ok(GetWeatherForcasts());

    [HttpGet("GenericClassActionNotFound")]
    public ActionResult<IEnumerable<WeatherForecast>> GenericClassActionNotFound()
        => NotFound();

    [HttpGet("InterfaceAction")]
    [ProducesResponseType(typeof(WeatherForecast[]), StatusCodes.Status200OK)]
    public IActionResult InterfaceAction()
        => Ok(GetWeatherForcasts());

    [HttpGet("ClassAction")]
    [ProducesResponseType(typeof(WeatherForecast[]), StatusCodes.Status200OK)]
    public ActionResult ClassAction()
        => Ok(GetWeatherForcasts());

    private static WeatherForecast[] GetWeatherForcasts()
        => Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            })
            .ToArray();
}
