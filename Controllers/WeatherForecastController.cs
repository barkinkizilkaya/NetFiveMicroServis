using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloDotNet5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherClient _weatherClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherClient weatherClient)
        {
            _logger = logger;
            _weatherClient = weatherClient;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> Get(string city)
        {
            var forecast = await _weatherClient.GetCurrentWeatherAsync(city);
            return new WeatherForecast
            {
                Summary = forecast.weather[0].description,
                TemperatureC = (int)forecast.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime
            };
        }
    }
}
