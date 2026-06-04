using System.Globalization;
using Playhouse.Core.Data.Repository;
using Playhouse.Core.Models;

namespace Playhouse.Core.Services.ApplicationSettingsService
{
    public sealed class SettingsService : ISettingsService, IInitializer
    {
        private readonly ISettingsRepository _repository;

        private ApplicationSettings _settings;

        public CultureInfo CurrentUICulture => _settings.UICulture;

        public string PathToData => _settings.PathToData;

        public IReadOnlySet<BrowserTypes> Browsers => _settings.Browsers.AsReadOnly();

        public IReadOnlySet<BrowserChannels> Channels => _settings.Channels.AsReadOnly();

        public event EventHandler<ISettingsService, EventArgs>? SettingsChanged;

        public SettingsService(ISettingsRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository);

            _repository = repository;
            _settings = new ApplicationSettings();
        }

        public async Task InitializeAsync()
        {
            _settings = await _repository.GetSettingsAsync().ConfigureAwait(false)
                ?? new ApplicationSettings();
            OnSettingsChanged();
        }

        public async Task SaveAsync(CultureInfo cultureUI, string pathToData, IEnumerable<BrowserTypes> browsers, IEnumerable<BrowserChannels> channels)
        {
            ArgumentNullException.ThrowIfNull(cultureUI);

            _settings.Update(cultureUI.Name, pathToData, browsers, channels);
            await _repository.UpdateSettingsAsync(_settings).ConfigureAwait(false);
            OnSettingsChanged();
        }

        private void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
