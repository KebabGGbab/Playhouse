using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Browsers
{
    public sealed class NotNullValidationSpecification : IValidationSpecification<BrowserType>
    {
        public Result IsSatisfiedBy(BrowserType browser)
        {
            return browser == null
                ? Result.Fail([new ApplicationSettingsBrowserNullError()])
                : Result.Ok();
        }
    }
}
