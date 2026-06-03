using System.Globalization;
using Playhouse.Core.Data.Repository;
using Playhouse.Core.Enums;

namespace Playhouse.Core.Services.ApplicationSettingsService
{
    public sealed class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;

        private ApplicationSettings _settings;

        public CultureInfo CurrentUICulture => _settings.UICulture;

        public string PathToData => _settings.PathToData;

        public IReadOnlySet<BrowserType> Browsers => _settings.Browsers.AsReadOnly();

        public IReadOnlySet<BrowserChannels> Channels => _settings.Channels.AsReadOnly();

        public event EventHandler<ISettingsService, EventArgs>? SettingsChanged;

        public SettingsService(ISettingsRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository);

            _repository = repository;
            _settings = new ApplicationSettings();
        }

        public async Task LoadAsync()
        {
            _settings = await _repository.GetSettingsAsync().ConfigureAwait(false)
                ?? new ApplicationSettings();
            OnSettingsChanged();
        }

        public async Task SaveAsync(CultureInfo cultureUI, string pathToData, IEnumerable<BrowserType> browsers, IEnumerable<BrowserChannels> channels)
        {
            ArgumentNullException.ThrowIfNull(cultureUI);

            ApplicationSettings newSettings = new(cultureUI.Name, pathToData, browsers, channels); 
            await _repository.UpdateSettingsAsync(newSettings).ConfigureAwait(false);
            OnSettingsChanged();
        }

        private void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
