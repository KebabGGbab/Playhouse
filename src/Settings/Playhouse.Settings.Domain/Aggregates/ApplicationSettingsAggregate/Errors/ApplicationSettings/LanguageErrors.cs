using System.Text;
using Playhouse.Settings.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;
using Root = Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;


namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;

public sealed class ApplicationSettingsLanguageNullError : Error
{
    public ApplicationSettingsLanguageNullError() 
        : base(ErrorMessages.ApplicationSettingsLanguageNotSpecified, "ApplicationSettings.Language.Null")
    {
    }
}

public sealed class ApplicationSettingsLanguageSameError : Error
{
    private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.ApplicationSettingsLanguageSame);

    public ApplicationSettingsLanguageSameError(Root.Culture culture) 
        : base(string.Format(null, _message, culture.Code), "ApplicationSettings.Language.Same")
    {
    }
}