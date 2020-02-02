namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public interface IWeatherForecastService : ICountable
    {
        IAsyncEnumerable<WeatherForecast> GetForecast(
            DateTime startDate,
            CancellationToken cancellationToken = default);
    }
}