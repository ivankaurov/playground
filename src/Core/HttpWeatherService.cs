namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

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

        public async Task<IReadOnlyCollection<WeatherForecast>> GetForecast(
            DateTime startDate,
            CancellationToken cancellationToken = default)
        {
            using var response = await this.client.GetAsync(
                                     $"{this.configuration.BaseUri}weather?startDate={startDate:yyyyMMdd}",
                                     HttpCompletionOption.ResponseHeadersRead,
                                     cancellationToken);

            response.EnsureSuccessStatusCode();
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<WeatherForecast[]>(
                       responseStream,
                       SerializerOptions,
                       cancellationToken);
        }
    }
}