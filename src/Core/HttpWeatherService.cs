namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class HttpWeatherService : CountableBase, IHttpWeatherForecastService
    {
        private static readonly JsonSerializerOptions SerializerOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };

        private readonly HttpClient client;

        public HttpWeatherService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IReadOnlyCollection<WeatherForecast>> GetForecast(
            DateTime startDate,
            CancellationToken cancellationToken = default)
        {
            using var response = await this.client.GetAsync(
                                     $"/weather?startDate={startDate:yyyyMMdd}",
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