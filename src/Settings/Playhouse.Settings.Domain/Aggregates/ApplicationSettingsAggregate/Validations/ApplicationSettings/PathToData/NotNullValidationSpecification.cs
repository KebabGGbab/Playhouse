using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.PathToData
{
    public sealed class NotNullValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath>
    {
        public Result IsSatisfiedBy(ApplicationSettingsAggregate.DirectoryPath path)
        {
            return path == null
                ? Result.Fail([new ApplicationSettingsPathToDataNullError()])
                : Result.Ok();
        }
    }
}
