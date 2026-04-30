using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Browsers
{
    public sealed class NotMissingValidationSpecification : IValidationSpecification<BrowserType>
    {
        private readonly IEnumerable<BrowserType> _browsers;

        public NotMissingValidationSpecification(IEnumerable<BrowserType> browsers) 
        {
            ArgumentNullException.ThrowIfNull(browsers);

            _browsers = browsers;
        }

        public Result IsSatisfiedBy(BrowserType browser)
        {
            return _browsers.Contains(browser) == false
                ? Result.Fail([new ApplicationSettingsBrowserNotFoundError(browser)])
                : Result.Ok();
        }
    }
}
