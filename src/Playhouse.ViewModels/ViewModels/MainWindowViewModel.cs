using CommunityToolkit.Mvvm.ComponentModel;

namespace Playhouse.ViewModels.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public RunViewModel RunViewModel { get; }

        public BotConfigurationsViewModel BotsViewModel { get; }

        public BrowserConfigurationsViewModel BrowsersViewModel { get; }

        public SettingsViewModel SettingsViewModel { get; }

        public MainWindowViewModel(RunViewModel runVM, BotConfigurationsViewModel botsVM, BrowserConfigurationsViewModel browsersVM, SettingsViewModel settingsVM)
        {
            ArgumentNullException.ThrowIfNull(runVM);
            ArgumentNullException.ThrowIfNull(botsVM);
            ArgumentNullException.ThrowIfNull(browsersVM);
            ArgumentNullException.ThrowIfNull(settingsVM);

            RunViewModel = runVM;
            BotsViewModel = botsVM;
            BrowsersViewModel = browsersVM;
            SettingsViewModel = settingsVM;
        }
    }
}
