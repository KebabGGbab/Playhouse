using Playhouse.Settings.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.DirectoryPath;

public sealed class DirectoryPathPathEmptyError : Error
{
    public DirectoryPathPathEmptyError()
        : base(ErrorMessages.DirectoryPathMustNotBeEmpty, "DirectoryPath.Path.Empty")
    {
    }
}

public sealed class DirectoryPathPathInvalidCharsError : Error
{
    public DirectoryPathPathInvalidCharsError()
        : base(ErrorMessages.DirectoryPathMustNotContainInvalidChars, "DirectoryPath.Path.InvalidChars")
    {
    }
}

public sealed class DirectoryPathPathRelativeError : Error
{
    public DirectoryPathPathRelativeError()
        : base(ErrorMessages.DirectoryPathMustBeAbsolute, "DirectoryPath.Path.Relative")
    {
    }
}