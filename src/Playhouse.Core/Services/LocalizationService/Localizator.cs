using System.Globalization;
using KebabGGbab.Localization.Manager;
using Playhouse.Core.Services.ApplicationSettingsService;

namespace Playhouse.Core.Services.LocalizationService
{
    public sealed class Localizator : ILocalizator, IInitializer
    {
        private readonly ILocalizationManager _localization;
        private readonly ISettingsService _settings;

        public IReadOnlyCollection<CultureInfo> SupportedUICultures => _localization.Cultures;

        public CultureInfo CurrentUICulture => _localization.CurrentUICulture;

        public Localizator(ILocalizationManager localization, ISettingsService settings)
        {
            ArgumentNullException.ThrowIfNull(localization);
            ArgumentNullException.ThrowIfNull(settings);

            _localization = localization;
            _settings = settings;
            _settings.SettingsChanged += SettingsChanged;
        }

        private void SettingsChanged(ISettingsService sender, EventArgs e)
        {
            SetCulture();
        }

        public async Task InitializeAsync()
        {
            SetCulture();
        }

        private void SetCulture()
        {
            _localization.CurrentUICulture = _settings.CurrentUICulture;
        }
    }
}
