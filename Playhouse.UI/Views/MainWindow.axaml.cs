using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.ViewModels.Messages;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views
{
    internal partial class MainWindow : Window
    {
        private readonly IWindowFactory _windowFactory = null!;

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
            WeakReferenceMessenger.Default.Register<MainWindow, GetBrowserEventsMessage>(this, OnGetBrowserEventsMessage);
        }

        private static void OnGetBrowserEventsMessage(MainWindow recepient, GetBrowserEventsMessage message)
        {
            recepient._windowFactory.CreateBotConstructorWindow(message.BrowserProfile, message.BotInfo).ShowDialog(recepient);

            message.Reply(message.BotInfo);
        }
    }
}