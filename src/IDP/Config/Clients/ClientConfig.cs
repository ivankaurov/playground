namespace Playground.IDP.Application.Config.Clients
{
    using System.Collections.Generic;

    public sealed class ClientConfig : IValidatableConfig
    {
        public string Id { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string Secret { get; set; }

        public string RedirectUri { get; set; }

        public IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                yield return nameof(this.Id) + " for client not set";
            }

            if (string.IsNullOrEmpty(this.DisplayName))
            {
                yield return nameof(this.DisplayName) + $" for client {this.Id} not set";
            }
        }
    }
}
