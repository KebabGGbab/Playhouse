using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.PathToData
{
    public sealed class CanPathToDataChangeValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath>
    {
        private readonly IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath> _validations;

        public CanPathToDataChangeValidationSpecification(ApplicationSettingsAggregate.ApplicationSettings settings)
        {
            _validations = new NotNullValidationSpecification()
                .Then(new NotAlreadySetValidationSpecification(settings));
        }

        public Result IsSatisfiedBy(ApplicationSettingsAggregate.DirectoryPath path)
        {
            return _validations.IsSatisfiedBy(path);
        }
    }
}
