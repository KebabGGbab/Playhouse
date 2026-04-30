using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Culture
{
    public sealed class CanCultureChangeValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.Culture>
    {
        private readonly IValidationSpecification<ApplicationSettingsAggregate.Culture> _validations;

        public CanCultureChangeValidationSpecification(ApplicationSettingsAggregate.ApplicationSettings settings)
        {
            _validations = new NotNullValidationSpecification()
                .Then(new NotAlreadySetValidationSpeification(settings));
        }

        public Result IsSatisfiedBy(ApplicationSettingsAggregate.Culture culture)
        {
            return _validations.IsSatisfiedBy(culture);
        }
    }
}
