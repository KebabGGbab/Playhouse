using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.Settings.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Culture
{
    public sealed class NotAlreadySetValidationSpeification : IValidationSpecification<ApplicationSettingsAggregate.Culture>
    {
        private readonly ApplicationSettingsAggregate.ApplicationSettings _settings;

        public NotAlreadySetValidationSpeification(ApplicationSettingsAggregate.ApplicationSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            _settings = settings;
        }

        public Result IsSatisfiedBy(ApplicationSettingsAggregate.Culture culture)
        {
            return _settings.Culture == culture
                ? Result.Fail([new ApplicationSettingsLanguageSameError(culture)])
                : Result.Ok();
        }
    }
}
