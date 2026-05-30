using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.ViewModels.ViewModels.EventBrowserViewModels;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class BotConstructorViewModel: ObservableObject
    {
        private readonly IBotConstructorFactory _constructorFactory;

        private AsyncRelayCommand? _startConstructorBotCommand;
        public BrowserEventViewModel? SelectedBrowserEvent 
        {
            get => field;
            set => SetProperty(ref field, value);
        }

        // placeholder
        public IEnumerable<BrowserEventViewModel> Events { get; set; }

        public BrowserProfileViewModel Profile { get; }

        public BotInfoViewModel Bot { get; }

        public event EventHandler? ConstructorCompleted;

        public ICommand StartConstructorBotCommand => _startConstructorBotCommand ??= new AsyncRelayCommand(StartConstructorBotExecuteAsync);

        public BotConstructorViewModel(IBotConstructorFactory constructorFactory, BrowserProfileViewModel profile, BotInfoViewModel bot)
        {
            ArgumentNullException.ThrowIfNull(constructorFactory, nameof(constructorFactory));
            ArgumentNullException.ThrowIfNull(profile, nameof(profile));
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));

            _constructorFactory = constructorFactory;
            Profile = profile;
            Bot = bot;
        }

        private void Constructor_ConstructionCompleted(object? sender, EventArgs e)
        {
            OnConstructorCompleted();
        }

        private void OnBrowserEventHappend(IBotConstructor sender, BrowserEventHappenedEventArgs e)
        {
            Bot.AddEvents(e.BrowserEvent);
        }

        private void OnConstructorCompleted()
        {
            ConstructorCompleted?.Invoke(this, EventArgs.Empty);
        }

        #region Method for commands

        private async Task StartConstructorBotExecuteAsync()
        {
            IBotConstructor constructor = _constructorFactory.Create(Profile.Profile, Bot.Bot);
            constructor.BrowserEventHappend += OnBrowserEventHappend;
            constructor.ConstructionCompleted += Constructor_ConstructionCompleted;
            await constructor.StartConstructorAsync().ConfigureAwait(true);
        }

        #endregion
    }
}
