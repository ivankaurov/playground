namespace Playground.Blazor.Core.Calc
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    internal sealed class HttpCalcService : IHttpCalcService
    {
        private readonly HttpClient httpClient;

        private readonly HttpCalcServiceConfiguration configuration;

        public HttpCalcService(HttpClient httpClient, IOptions<HttpCalcServiceConfiguration> configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration.Value;
        }

        public async Task<double> Multiply(double a, double b, CancellationToken cancellationToken = default)
        {
            using var response = await this.httpClient.GetAsync(
                                     $"{this.configuration.BaseUri}calc/{a}/multiply/{b}",
                                     HttpCompletionOption.ResponseHeadersRead,
                                     cancellationToken);

            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<double>(stream, cancellationToken: cancellationToken);
        }
    }
}