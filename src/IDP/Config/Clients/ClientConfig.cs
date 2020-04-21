namespace Playground.IDP.Application.Config.Clients
{
    using System.Collections.Generic;

    public sealed class ClientConfig : IValidatableConfig
    {
        public string Id { get; set; } = null!;

        public string DisplayName { get; set; } = null!;
        \
        public IEnumerable<string> Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
