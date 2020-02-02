namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    internal sealed class WeatherForecastService : CountableBase, IWeatherForecastService
    {
        private static readonly Random Rnd = new Random();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        public IAsyncEnumerable<WeatherForecast> GetForecast(DateTime startDate, CancellationToken cancellationToken = default)
        {
            return Enumerable.Range(1, 5).Select(
                index => new WeatherForecast
                             {
                                 Date = startDate.AddDays(index),
                                 TemperatureC = Rnd.Next(-20, 55),
                                 Summary = Summaries[Rnd.Next(Summaries.Length)],
                             }).ToAsyncEnumerable();
        }
    }
}
