using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings;

public sealed class ApplicationSettingsBrowserNotNullValidationSpecification : IValidationSpecification<BrowserType>
{
    public Result IsSatisfiedBy(BrowserType browser)
    {
        return browser == null
            ? Result.Fail([new ApplicationSettingsBrowserNullError()])
            : Result.Ok();
    }
}

public sealed class ApplicationSettingsBrowserNotAlreadyAddedValidaitonSpecification : IValidationSpecification<BrowserType>
{
    private readonly IEnumerable<BrowserType> _browsers;

    public ApplicationSettingsBrowserNotAlreadyAddedValidaitonSpecification(IEnumerable<BrowserType> browsers)
    {
        ArgumentNullException.ThrowIfNull(browsers);

        _browsers = browsers;
    }

    public Result IsSatisfiedBy(BrowserType browser)
    {
        return _browsers.Contains(browser)
            ? Result.Fail([new ApplicationSettingsBrowserExistError(browser)])
            : Result.Ok();
    }
}

public sealed class ApplicationSettingsBrowserNotMissingValidationSpecification : IValidationSpecification<BrowserType>
{
    private readonly IEnumerable<BrowserType> _browsers;

    public ApplicationSettingsBrowserNotMissingValidationSpecification(IEnumerable<BrowserType> browsers)
    {
        ArgumentNullException.ThrowIfNull(browsers);

        _browsers = browsers;
    }

    public Result IsSatisfiedBy(BrowserType browser)
    {
        return _browsers.Contains(browser) == false
            ? Result.Fail([new ApplicationSettingsBrowserNotFoundError(browser)])
            : Result.Ok();
    }
}

public sealed class ApplicationSettingsBrowserCanAddValidationSpecification : IValidationSpecification<BrowserType>
{
    private readonly IValidationSpecification<BrowserType> _validations;

    public ApplicationSettingsBrowserCanAddValidationSpecification(IEnumerable<BrowserType> browsers)
    {
        _validations = new ApplicationSettingsBrowserNotNullValidationSpecification()
            .Then(new ApplicationSettingsBrowserNotAlreadyAddedValidaitonSpecification(browsers));
    }

    public Result IsSatisfiedBy(BrowserType browser)
    {
        return _validations.IsSatisfiedBy(browser);
    }
}

public sealed class ApplicationSettingsBrowserCanRemoveValidationSpecification
{
    private readonly IValidationSpecification<BrowserType> _validations;

    public ApplicationSettingsBrowserCanRemoveValidationSpecification(IEnumerable<BrowserType> browsers)
    {
        _validations = new ApplicationSettingsBrowserNotNullValidationSpecification()
            .Then(new ApplicationSettingsBrowserNotMissingValidationSpecification(browsers));
    }

    public Result IsSatisfiedBy(BrowserType browser)
    {
        return _validations.IsSatisfiedBy(browser);
    }
}
