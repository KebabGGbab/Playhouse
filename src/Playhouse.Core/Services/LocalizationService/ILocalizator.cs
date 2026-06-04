using System.Globalization;

namespace Playhouse.Core.Services.LocalizationService
{
    public interface ILocalizator
    {
        IReadOnlyCollection<CultureInfo> SupportedUICultures { get; }

        CultureInfo CurrentUICulture { get; }
    }
}
