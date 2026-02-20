using CommunityToolkit.Mvvm.ComponentModel;

namespace Playhouse.ViewModels.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public RunViewModel RunViewModel { get; }

        public BotsInfoViewModel BotsInfoViewModel { get; }

        public BrowserProfilesViewModel BrowserProfilesViewModel { get; }

        public SettingsViewModel SettingsViewModel { get; }

        public MainWindowViewModel(RunViewModel runVM, BotsInfoViewModel botsVM, BrowserProfilesViewModel profilesVM, SettingsViewModel settingsVM)
        {
            ArgumentNullException.ThrowIfNull(runVM, nameof(runVM));
            ArgumentNullException.ThrowIfNull(botsVM, nameof(botsVM));
            ArgumentNullException.ThrowIfNull(profilesVM, nameof(profilesVM));
            ArgumentNullException.ThrowIfNull(settingsVM, nameof(settingsVM));

            RunViewModel = runVM;
            BotsInfoViewModel = botsVM;
            BrowserProfilesViewModel = profilesVM;
            SettingsViewModel = settingsVM;
        }
    }
}
