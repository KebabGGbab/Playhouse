using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Culture
{
    public sealed class NotNullValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.Culture>
    {
        public Result IsSatisfiedBy(ApplicationSettingsAggregate.Culture culture)
        {
            return culture == null
                ? Result.Fail([new ApplicationSettingsLanguageNullError()])
                : Result.Ok();
        }
    }
}
