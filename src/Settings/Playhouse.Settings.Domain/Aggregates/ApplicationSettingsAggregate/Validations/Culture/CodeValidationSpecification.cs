using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.Culture;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.Culture;

public sealed class CultureCodeNotEmptyValidationSpecification : IValidationSpecification<string>
{
    public Result IsSatisfiedBy(string code)
    {
        return string.IsNullOrWhiteSpace(code)
            ? Result.Fail([new CultureCodeEmptyError()])
            : Result.Ok();
    }
}

public sealed class CultureCodeNotSupportedValidationSpecification : IValidationSpecification<string>
{
    private readonly IEnumerable<string> _supportedCultures;

    public CultureCodeNotSupportedValidationSpecification(IEnumerable<string> supportedCultures)
    {
        ArgumentNullException.ThrowIfNull(supportedCultures);

        _supportedCultures = supportedCultures;
    }

    public Result IsSatisfiedBy(string code)
    {
        return _supportedCultures.Contains(code) == false
            ? Result.Fail([new CultureCodeUnsupportedError(code)])
            : Result.Ok();
    }
}

public sealed class CodeValidationSpecification : IValidationSpecification<string>
{
    private readonly IValidationSpecification<string> _validators;

    public CodeValidationSpecification(IEnumerable<string> supportedCultures)
    {
        _validators = new CultureCodeNotSupportedValidationSpecification(supportedCultures);
    }

    public Result IsSatisfiedBy(string code)
    {
        return _validators.IsSatisfiedBy(code);
    }
}
