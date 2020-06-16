namespace Playground.IDP.Application.Config.Resources
{
    using System.Collections.Generic;

    public sealed class ResourceConfig : IValidatableConfig
    {
        public string Id { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                yield return nameof(this.Id) + " for resource not set";
            }

            if (string.IsNullOrEmpty(this.DisplayName))
            {
                yield return nameof(this.DisplayName) + $" for resource {this.Id} not set";
            }
        }
    }
}