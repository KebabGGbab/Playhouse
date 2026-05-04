using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings;

public sealed class ApplicationSettingsLanguageNotNullValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.Culture>
{
    public Result IsSatisfiedBy(ApplicationSettingsAggregate.Culture culture)
    {
        return culture == null
            ? Result.Fail([new ApplicationSettingsLanguageNullError()])
            : Result.Ok();
    }
}

public sealed class ApplicationSettingsLanguageNotAlreadySetValidationSpeification : IValidationSpecification<ApplicationSettingsAggregate.Culture>
{
    private readonly ApplicationSettingsAggregate.ApplicationSettings _settings;

    public ApplicationSettingsLanguageNotAlreadySetValidationSpeification(ApplicationSettingsAggregate.ApplicationSettings settings)
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

public sealed class ApplicationSettingsLanguageCanChangeValidationSpecification : IValidationSpecification<ApplicationSettingsAggregate.Culture>
{
    private readonly IValidationSpecification<ApplicationSettingsAggregate.Culture> _validations;

    public ApplicationSettingsLanguageCanChangeValidationSpecification(ApplicationSettingsAggregate.ApplicationSettings settings)
    {
        _validations = new ApplicationSettingsLanguageNotNullValidationSpecification()
            .Then(new ApplicationSettingsLanguageNotAlreadySetValidationSpeification(settings));
    }

    public Result IsSatisfiedBy(ApplicationSettingsAggregate.Culture culture)
    {
        return _validations.IsSatisfiedBy(culture);
    }
}