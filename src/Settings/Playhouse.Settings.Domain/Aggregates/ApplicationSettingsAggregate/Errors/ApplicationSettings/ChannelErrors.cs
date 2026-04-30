using System.Text;
using Playhouse.Settings.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;

public sealed class ApplicationSettingsChannelNullError : Error
{
    public ApplicationSettingsChannelNullError()
        : base(ErrorMessages.ApplicationSettingsChannelNotSpecified, "ApplicationSettings.Channel.Null")
    {
    }
}

public sealed class ApplicationSettingsChannelExistError : Error
{
    private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.ApplicationSettingsChannelExist);

    public ApplicationSettingsChannelExistError(BrowserChannel channel) 
        : base(string.Format(null, _message, channel.Name, channel.OwnerBrowser.Name), "ApplicationSettings.Channel.Exist")
    {
    }
}

public sealed class ApplicationSettingsChannelNotFoundError : Error
{
    private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.ApplicationSettingsChannelNotFound);

    public ApplicationSettingsChannelNotFoundError(BrowserChannel channel) 
        : base(string.Format(null, _message, channel.Name, channel.OwnerBrowser.Name), "ApplicationSettings.Channel.NotFound")
    {
    }
}