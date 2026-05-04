using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings;

public sealed class ApplicationSettingsPathToDataNotNullValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath>
{
    public Result IsSatisfiedBy(ApplicationSettingsAggregate.DirectoryPath path)
    {
        return path == null
            ? Result.Fail([new ApplicationSettingsPathToDataNullError()])
            : Result.Ok();
    }
}

public sealed class ApplicationSettingsPathToDataNotAlreadySetValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath>
{
    private readonly ApplicationSettingsAggregate.ApplicationSettings _settings;

    public ApplicationSettingsPathToDataNotAlreadySetValidationSpecification(ApplicationSettingsAggregate.ApplicationSettings settings)
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

public sealed class ApplicationSettingsPathToDataCanChangeValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath>
{
    private readonly IValidationSpecification<ApplicationSettingsAggregate.DirectoryPath> _validations;

    public ApplicationSettingsPathToDataCanChangeValidationSpecification(ApplicationSettingsAggregate.ApplicationSettings settings)
    {
        _validations = new ApplicationSettingsPathToDataNotNullValidationSpecification()
            .Then(new ApplicationSettingsPathToDataNotAlreadySetValidationSpecification(settings));
    }

    public Result IsSatisfiedBy(ApplicationSettingsAggregate.DirectoryPath path)
    {
        return _validations.IsSatisfiedBy(path);
    }
}