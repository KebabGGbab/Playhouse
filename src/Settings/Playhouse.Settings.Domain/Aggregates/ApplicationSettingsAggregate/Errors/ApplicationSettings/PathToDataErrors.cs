using System.Text;
using Playhouse.Settings.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;
using Root = Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;

public sealed class ApplicationSettingsPathToDataNullError : Error
{
    public ApplicationSettingsPathToDataNullError() 
        : base(ErrorMessages.ApplicationSettingsPathToDataNotSpecified, "ApplicationSettings.PathToData.Null")
    {
    }
}

public sealed class ApplicationSettingsPathToDataSameError : Error
{
    private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.ApplicationSettingsPathToDataSame);

    public ApplicationSettingsPathToDataSameError(Root.DirectoryPath pathToData) 
        : base(string.Format(null, _message, pathToData.Path), "ApplicationSettings.PathToData.Same")
    {
    }
}
