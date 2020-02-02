namespace Playground.Blazor.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    using Microsoft.AspNetCore.Mvc;

    using Playground.Blazor.Core;

    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherForecastService weatherForecastService;

        public WeatherController(IWeatherForecastService weatherForecastService)
        {
            this.weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public IAsyncEnumerable<WeatherForecast> GetForecast(
            [FromQuery] string startDate,
            CancellationToken cancellationToken)
        {
            return this.weatherForecastService.GetForecast(
                DateTime.ParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture),
                cancellationToken);
        }
    }
}