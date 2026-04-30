using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.DirectoryPath;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.DirectoryPath.Path
{
    public sealed class AbsoluteValidationSpecification : IValidationSpecification<string>
    {
        public Result IsSatisfiedBy(string path)
        {
            return System.IO.Path.IsPathFullyQualified(path) == false
                ? Result.Fail([new DirectoryPathPathRelativeError()])
                : Result.Ok();
        }
    }
}
