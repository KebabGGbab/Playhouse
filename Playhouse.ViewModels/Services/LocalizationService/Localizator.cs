using System.Globalization;
using KebabGGbab.Localization.Abstractions;
using Playhouse.ViewModels.Services.LocalizationService.Abstractions;

namespace Playhouse.ViewModels.Services.LocalizationService
{
    public sealed class Localizator : ILocalizator
    {
        private readonly ILocalizationManager _manager;

        public IReadOnlyList<CultureInfo> Cultures => _manager.Cultures;
        public CultureInfo CurrentUICulture => _manager.CurrentUICulture;

        public Localizator(ILocalizationManager manager)
        {
            ArgumentNullException.ThrowIfNull(manager, nameof(manager));

            _manager = manager;
        }

        public void SetUICulture(CultureInfo culture)
        {
            ArgumentNullException.ThrowIfNull(culture, nameof(culture));

            _manager.CurrentUICulture = culture;
        } 
    }
}
