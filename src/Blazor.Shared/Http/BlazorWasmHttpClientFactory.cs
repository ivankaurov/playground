namespace Playground.Blazor.Shared.Http
{
    using System.Net.Http;

    internal sealed class BlazorWasmHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient httpClient;

        public BlazorWasmHttpClientFactory(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public HttpClient CreateClient(string name) => this.httpClient;
    }
}