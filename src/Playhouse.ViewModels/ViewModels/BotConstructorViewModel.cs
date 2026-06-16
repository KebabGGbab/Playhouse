using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Playhouse.Application.Services.ConstructorService.Abstractions;
using Playhouse.ViewModels.ViewModels.BotActionViewModels;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class BotConstructorViewModel: ObservableObject
    {
        private readonly IConstructor _constructor;

        public BotActionViewModel? SelectedAction 
        {
            get;
            set => SetProperty(ref field, value);
        }

        public BrowserConfigurationViewModel Profile { get; }

        public BotConfigurationViewModel Bot { get; }

        public event EventHandler? ConstructorCompleted;

        public IAsyncRelayCommand StartConstructionCommand { get; }
        public IAsyncRelayCommand CompleteConstructionCommand { get;}

        public BotConstructorViewModel(IConstructorFactory constructorFactory, BrowserConfigurationViewModel profile, BotConfigurationViewModel bot)
        {
            ArgumentNullException.ThrowIfNull(constructorFactory);
            ArgumentNullException.ThrowIfNull(profile);
            ArgumentNullException.ThrowIfNull(bot);

            _constructor = constructorFactory.Create(profile.Profile, bot.Bot);
            Profile = profile;
            Bot = bot;
            StartConstructionCommand = new AsyncRelayCommand(StartConstructionAsync);
            CompleteConstructionCommand = new AsyncRelayCommand(CompleteConstructionAsync);
        }

        private void Constructor_ConstructionCompleted(object? sender, EventArgs e)
        {
            OnConstructorCompleted();
        }

        private void OnActionHappend(IConstructor sender, BrowserEventHappenedEventArgs e)
        {
            Bot.AddAction(e.Action);
        }

        private void OnConstructorCompleted()
        {
            ConstructorCompleted?.Invoke(this, EventArgs.Empty);
        }

        private async Task StartConstructionAsync()
        {
            _constructor.ActionHappend += OnActionHappend;
            _constructor.ConstructionCompleted += Constructor_ConstructionCompleted;
            await _constructor.StartConstructionAsync().ConfigureAwait(true);
        }

        private async Task CompleteConstructionAsync()
        {
            await _constructor.CompleteConstructionAsync().ConfigureAwait(true);
            _constructor.ActionHappend -= OnActionHappend;
            _constructor.ConstructionCompleted -= Constructor_ConstructionCompleted;
        }
    }
}
