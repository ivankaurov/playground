namespace Playground.IDP.Application.Config
{
    using System.Collections.Generic;
    using System.Linq;

    using Playground.IDP.Application.Config.Clients;
    using Playground.IDP.Application.Config.Resources;

    public sealed class SettingsConfig : IValidatableConfig
    {
        public IList<ResourceConfig> Resources { get; } = new List<ResourceConfig>();

        public IList<ClientConfig> Clients { get; } = new List<ClientConfig>();

        public IEnumerable<string> Validate()
        {
            if (this.Resources.Count == 0)
            {
                yield return "No resources configured";
            }

            if (this.Clients.Count == 0)
            {
                yield return "No clients configured";
            }

            foreach (var validationError in this.Resources.SelectMany(r => r.Validate())
                .Concat(this.Clients.SelectMany(c => c.Validate())))
            {
                yield return validationError;
            }
        }
    }
}