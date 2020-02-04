namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text.Json;
    using System.Threading;

    using Microsoft.Extensions.Options;

    internal sealed class HttpWeatherService : CountableBase, IHttpWeatherForecastService
    {
        private static readonly JsonSerializerOptions SerializerOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };

        private readonly HttpClient client;

        private readonly HttpWeatherServiceConfiguration configuration;

        public HttpWeatherService(HttpClient client, IOptions<HttpWeatherServiceConfiguration> configuration)
        {
            this.client = client;
            this.configuration = configuration.Value;
        }

        public async IAsyncEnumerable<WeatherForecast> GetForecast(
            DateTime startDate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var response = await this.client.GetAsync(
                                     $"{this.configuration.BaseUri}weather?startDate={startDate:yyyyMMdd}",
                                     HttpCompletionOption.ResponseHeadersRead,
                                     cancellationToken);

            response.EnsureSuccessStatusCode();
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            foreach (var forecastElement in await JsonSerializer.DeserializeAsync<WeatherForecast[]>(
                                                responseStream,
                                                SerializerOptions,
                                                cancellationToken))
            {
                yield return forecastElement;
            }
        }
    }
}