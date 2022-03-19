using AutoFilterer.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPITest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
        public IEnumerable<WeatherForecast> Get([FromQuery] WeatherForecastFilter filter)
        {
            var rng = new Random();
            // Change range to 100 from 5 to get more reasonable results.
            return Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
          .AsQueryable().ApplyFilter(filter)// π”√filter
            .ToArray();
        }

        [HttpGet(Name = "GetWeatherForecastA")]
        public IEnumerable<WeatherForecast> GetA()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "GetWeatherForecastB")]
        [ProducesDefaultResponseType(typeof(WeatherForecast))]
        public IEnumerable<dynamic> GetB([FromQuery] DynamicLinqDto dto)
        {
            var rng = new Random();
            IQueryable query = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .AsQueryable();

            return query.ToDynamicArray(dto);
        }
    }
}