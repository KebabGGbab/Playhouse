using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using Playhouse.Core.Enums;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;
using Playhouse.Core.Services.SettingsService.Abstractions;
using Playhouse.ViewModels.Services.LocalizationService.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class SettingsViewModel : ObservableObject
    {
        private readonly ISettingsUpdater<UserSettings> _settingsUpdater;
        private readonly IPlaywrightBrowserInstaller _playwrightBrowserInstaller;
        private readonly ILocalizator _localizator;
        private readonly UserSettings _userSettings;

        public IReadOnlyList<CultureInfo> Cultures => _localizator.Cultures; 

        public CultureInfo SelectedCulture 
        { 
            get => CultureInfo.GetCultureInfo(_userSettings.Culture.Name);
            set => SetProperty(_userSettings.Culture.Name, value.Name, _userSettings, (m, v) => m.Culture.Name = v);
        }

        public string PathToProfiles
        {
            get => _userSettings.FileLocations.Profiles;
            set => SetProperty(_userSettings.FileLocations.Profiles, value, _userSettings, (m, v) => m.FileLocations.Profiles = v);
        }

        public string PathToBots
        {
            get => _userSettings.FileLocations.Bots;
            set => SetProperty(_userSettings.FileLocations.Bots, value, _userSettings, (m, v) => m.FileLocations.Bots = v);
        }

        public string DefaultProfileName
        {
            get => _userSettings.Entity.DefaultProfileName;
            set => SetProperty(_userSettings.Entity.DefaultProfileName, value, _userSettings, (m, v) => m.Entity.DefaultProfileName = v);
        }
        public string DefaultBotName
        {
            get => _userSettings.Entity.DefaultBotName;
            set => SetProperty(_userSettings.Entity.DefaultBotName, value, _userSettings, (m, v) => m.Entity.DefaultBotName = v);
        }

        public BrowserType InstalledBrowsers
        {
            get => _userSettings.Playwright.BrowserTypes;
            set => SetProperty(_userSettings.Playwright.BrowserTypes, value, _userSettings, (m, v) => m.Playwright.BrowserTypes = v);
        }

        public BrowserChannels InstalledChannels
        {
            get => _userSettings.Playwright.Channels;
            set => SetProperty(_userSettings.Playwright.Channels, value, _userSettings, (m, v) => m.Playwright.Channels = v);
        }

        public ICommand SaveSettings
        {
            get => field ??= new RelayCommand(ExecuteSaveSettings);
        }

		public SettingsViewModel(IOptions<UserSettings> userSettings, ISettingsUpdater<UserSettings> settingsUpdater, IPlaywrightBrowserInstaller playwrightBrowserInstaller, ILocalizator localizator) 
        {
            _settingsUpdater = settingsUpdater;
            _playwrightBrowserInstaller = playwrightBrowserInstaller;
            _localizator = localizator;
            _userSettings = userSettings.Value;
            _localizator.SetUICulture(SelectedCulture);
        }

        private async void ExecuteSaveSettings()
        {
            _localizator.SetUICulture(SelectedCulture);
            Task[] updateTasks = [
                _settingsUpdater.UpdateAsync(_userSettings), 
                _playwrightBrowserInstaller.InstallAsync()];
            await Task.WhenAll(updateTasks);
        }
	}
}
