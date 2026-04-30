using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.DirectoryPath;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.DirectoryPath.Path
{
    public sealed class NotEmptyValidationSpecification : IValidationSpecification<string>
    {
        public Result IsSatisfiedBy(string path)
        {
            return string.IsNullOrWhiteSpace(path)
                ? Result.Fail([new DirectoryPathPathEmptyError()])
                : Result.Ok();
        }
    }
}
