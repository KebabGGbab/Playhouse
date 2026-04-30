using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.PathToData
{
    public sealed class NotAlreadySetValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath>
    {
        private readonly ApplicationSettingsAggregate.ApplicationSettings _settings;

        public NotAlreadySetValidationSpecification(ApplicationSettingsAggregate.ApplicationSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            _settings = settings;
        }

        public Result IsSatisfiedBy(ApplicationSettingsAggregate.DirectoryPath path)
        {
            return _settings.PathToData == path
                ? Result.Fail([new ApplicationSettingsPathToDataSameError(path)])
                : Result.Ok();
        }
    }
}
