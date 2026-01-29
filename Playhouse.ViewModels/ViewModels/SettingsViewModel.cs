using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.ConfigurationService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;
using Playhouse.ViewModels.Services.LocalizationService.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class SettingsViewModel : ObservableObject
    {
        private readonly IConfigurationUpdater _configurationUpdater;
        private readonly IPlaywrightBrowserInstaller _playwrightBrowserInstaller;
        private readonly ILocalizator _localizator;

        public UserSettings UserSettings
        {
            get => field;
            private set => SetProperty(ref field, value);
        }

        public IReadOnlyList<CultureInfo> Cultures => _localizator.Cultures; 
        public CultureInfo SelectedCulture 
        { 
            get => CultureInfo.GetCultureInfo(UserSettings.View.Language);
            set => SetProperty(UserSettings.View.Language, value.Name, UserSettings, (s, l) => s.View.Language = l);
        }

        #region Commands

        public ICommand SaveSettings
        {
            get => field ??= new RelayCommand(ExecuteSaveSettings);
        }

		#endregion

		public SettingsViewModel(IOptionsMonitor<UserSettings> userSettings, IConfigurationUpdater configurationUpdater, IPlaywrightBrowserInstaller playwrightBrowserInstaller, ILocalizator localizator) 
        {
            _configurationUpdater = configurationUpdater;
            _playwrightBrowserInstaller = playwrightBrowserInstaller;
            _localizator = localizator;

            UserSettings = userSettings.CurrentValue;
            SelectedCulture = Cultures.First(c => c.Name == userSettings.CurrentValue.View.Language);
            userSettings.OnChange(s =>
                {
                    UserSettings = s;
                    SelectedCulture = Cultures.First(c => c.Name == s.View.Language);
                });
        }

		#region Methods For Commands

        private async void ExecuteSaveSettings()
        {
            Task updateConfigTask = _configurationUpdater.UpdateAsync((c) => UserSettings);
            _playwrightBrowserInstaller.Install();
            _localizator.SetUICulture(SelectedCulture);
            await updateConfigTask.ConfigureAwait(false);
        }

		#endregion
	}
}
