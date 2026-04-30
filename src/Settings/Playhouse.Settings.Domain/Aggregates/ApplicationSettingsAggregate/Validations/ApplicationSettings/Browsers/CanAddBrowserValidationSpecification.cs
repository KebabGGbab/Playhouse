using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Browsers
{
    public sealed class CanAddBrowserValidationSpecification : IValidationSpecification<BrowserType>
    {
        private readonly IValidationSpecification<BrowserType> _validations;

        public CanAddBrowserValidationSpecification(IEnumerable<BrowserType> browsers)
        {
            _validations = new NotNullValidationSpecification()
                .Then(new NotAlreadyAddedValidaitonSpecification(browsers));
        }

        public Result IsSatisfiedBy(BrowserType browser)
        {
            return _validations.IsSatisfiedBy(browser);
        }
    }
}
