namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IHttpWeatherForecastService : ICountable
    {
        Task<IReadOnlyCollection<WeatherForecast>> GetForecast(
            DateTime startDate,
            CancellationToken cancellationToken = default);
    }
}