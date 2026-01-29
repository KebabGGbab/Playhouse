using Avalonia.Controls;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views
{
    internal partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор для дизайнера
        /// </summary>
        public MainWindow()
        {
            if (!Design.IsDesignMode)
            {
                throw new InvalidOperationException();
            }

            InitializeComponent();
        }

        public MainWindow(RunViewModel runViewModel, BotViewModel botViewModel, ProfilesViewModel profileViewModel, SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            TabRun.DataContext = runViewModel;
            TabBots.DataContext = botViewModel;
            TabProfiles.DataContext = profileViewModel;
            TabSettings.DataContext = settingsViewModel;
        }
    }
}