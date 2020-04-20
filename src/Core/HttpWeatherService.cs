namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text.Json;
    using System.Threading;

    internal sealed class HttpWeatherService : CountableBase, IHttpWeatherForecastService
    {
        public const string HttpClientName = nameof(HttpWeatherService);

        private static readonly JsonSerializerOptions SerializerOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };

        private readonly IHttpClientFactory httpClientFactory;

        public HttpWeatherService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async IAsyncEnumerable<WeatherForecast> GetForecast(
            DateTime startDate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var client = this.httpClientFactory.CreateClient(HttpClientName);
            using var response = await client.GetAsync(
                                     $"weather?startDate={startDate:yyyyMMdd}",
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