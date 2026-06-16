using System.Globalization;

namespace Playhouse.Application.Services.LocalizationService
{
    public interface ILocalizator
    {
        IReadOnlyCollection<CultureInfo> SupportedUICultures { get; }

        CultureInfo CurrentUICulture { get; }
    }
}
