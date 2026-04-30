using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.Culture;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.Culture.Code
{
    public sealed class NotSupportedValidationSpecification : IValidationSpecification<string>
    {
        private readonly IEnumerable<string> _supportedCultures;

        public NotSupportedValidationSpecification(IEnumerable<string> supportedCultures)
        {
            ArgumentNullException.ThrowIfNull(supportedCultures);

            _supportedCultures = supportedCultures;
        }

        public Result IsSatisfiedBy(string code)
        {
            return _supportedCultures.Contains(code) == false
                ? Result.Fail([new CultureCodeUnsupportedError(code)])
                : Result.Ok();
        }
    }
}
