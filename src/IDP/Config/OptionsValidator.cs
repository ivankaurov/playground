namespace Playground.IDP.Application.Config
{
    using System.Linq;
    using Microsoft.Extensions.Options;

    internal sealed class OptionsValidator<TOptions> : IValidateOptions<TOptions>
        where TOptions : class, IValidatableConfig
    {
        public ValidateOptionsResult Validate(string name, TOptions options)
        {
            var validationErrors = options.Validate().ToList();
            return validationErrors.Count > 0
                ? ValidateOptionsResult.Fail(validationErrors)
                : ValidateOptionsResult.Success;
        }
    }
}