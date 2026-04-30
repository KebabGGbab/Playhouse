using System.Text;
using Playhouse.Settings.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.Culture;

public sealed class CultureCodeEmptyError : Error
{
    public CultureCodeEmptyError()
        : base(ErrorMessages.CultureCodeMustNotBeEmpty, "Culture.Code.Empty")
    {
    }
}

public sealed class CultureCodeUnsupportedError : Error
{
    private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.CultureCodeMustBeSupported);

    public CultureCodeUnsupportedError(string cultureCode)
        : base(string.Format(null, _message, cultureCode), "Culture.Code.Unsupported")
    {
    }
}