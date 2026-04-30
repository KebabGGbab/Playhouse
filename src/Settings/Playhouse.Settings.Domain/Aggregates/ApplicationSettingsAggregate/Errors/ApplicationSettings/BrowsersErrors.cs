using System.Text;
using Playhouse.Settings.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;

public sealed class ApplicationSettingsBrowserNullError : Error
{
    public ApplicationSettingsBrowserNullError()
        : base(ErrorMessages.ApplicationSettingsBrowserNotSpecified, "ApplicationSettings.Browser.Null")
    {
    }
}

public sealed class ApplicationSettingsBrowserExistError : Error
{
    private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.ApplicationSettingsBrowserExist);

    public ApplicationSettingsBrowserExistError(BrowserType browser) 
        : base(string.Format(null, _message, browser.Name), "ApplicationSettings.Browser.Exist")
    {
    }
}

public sealed class ApplicationSettingsBrowserNotFoundError : Error
{
    private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.ApplicationSettingsBrowserNotFound);

    public ApplicationSettingsBrowserNotFoundError(BrowserType browser) 
        : base(string.Format(null, _message, browser.Name), "ApplicationSettings.Browser.NotFound")
    {

    }
}