using KebabGGbab.Primitives.Extensions;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.DirectoryPath;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.DirectoryPath.Path
{
    public sealed class HasNotInvalidCharsValidationSpecifications : IValidationSpecification<string>
    {
        public Result IsSatisfiedBy(string path)
        {
            return System.IO.Path.CheckHasInvalidChars(path)
                ? Result.Fail([new DirectoryPathPathInvalidCharsError()])
                : Result.Ok();
        }
    }
}
