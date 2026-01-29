using System.Globalization;

namespace Playhouse.ViewModels.Services.LocalizationService.Abstractions
{
    public interface ILocalizator
    {
        IReadOnlyList<CultureInfo> Cultures { get; }
        CultureInfo CurrentUICulture { get; }

        void SetUICulture(CultureInfo culture);
    }
}
