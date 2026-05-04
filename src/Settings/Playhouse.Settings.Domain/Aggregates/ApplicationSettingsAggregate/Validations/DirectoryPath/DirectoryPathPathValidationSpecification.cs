using KebabGGbab.Primitives.Extensions;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.DirectoryPath;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.DirectoryPath;

public sealed class DirectoryPathPathNotEmptyValidationSpecification : IValidationSpecification<string>
{
    public Result IsSatisfiedBy(string path)
    {
        return string.IsNullOrWhiteSpace(path)
            ? Result.Fail([new DirectoryPathPathEmptyError()])
            : Result.Ok();
    }
}

public sealed class DirectoryPathPathAbsoluteValidationSpecification : IValidationSpecification<string>
{
    public Result IsSatisfiedBy(string path)
    {
        return System.IO.Path.IsPathFullyQualified(path) == false
            ? Result.Fail([new DirectoryPathPathRelativeError()])
            : Result.Ok();
    }
}

public sealed class DirectoryPathPathHasNotInvalidCharsValidationSpecifications : IValidationSpecification<string>
{
    public Result IsSatisfiedBy(string path)
    {
        return System.IO.Path.CheckHasInvalidChars(path)
            ? Result.Fail([new DirectoryPathPathInvalidCharsError()])
            : Result.Ok();
    }
}

public sealed class DirectoryPathPathValidationSpecification : IValidationSpecification<string>
{
    private readonly IValidationSpecification<string> _validations;

    public DirectoryPathPathValidationSpecification()
    {
        _validations = new DirectoryPathPathHasNotInvalidCharsValidationSpecifications()
            .And(new DirectoryPathPathAbsoluteValidationSpecification());
    }

    public Result IsSatisfiedBy(string path)
    {
        return _validations.IsSatisfiedBy(path);
    }
}