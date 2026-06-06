using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.ViewModels.Messages;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views.Windows
{
    internal partial class MainWindow : Window
    {
        private readonly IWindowFactory _windowFactory = null!;
        private readonly MainWindowViewModel _vm = null!;

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

        public MainWindow(IWindowFactory windowFactory, MainWindowViewModel vm)
        {
            _windowFactory = windowFactory;
            InitializeComponent();
            DataContext = vm;
            WeakReferenceMessenger.Default.Register<MainWindow, GetBotActionsMessage>(this, OnGetBotActionsMessage);
        }

        private static void OnGetBotActionsMessage(MainWindow recepient, GetBotActionsMessage message)
        {
            message.Reply(recepient._windowFactory
                .CreateBotConstructorWindow(message.Profile, message.Bot)
                .ShowDialog<BotConfigurationViewModel>(recepient));
        }

        private void Window_Loaded(object? sender, RoutedEventArgs e)
        {
            if (Design.IsDesignMode)
            {
                return;
            }

            _vm.SettingsViewModel.InitializeCommand.Execute(null);
        }
    }
}