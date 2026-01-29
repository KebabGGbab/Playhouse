using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Services.BotConstructorService.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class BotConstructorViewModel: ObservableObject
    {
        private readonly IBotConstructor _constructor;

        public BrowserEvent? SelectBrowserEvent 
        {
            get => field;
            set => SetProperty(ref field, value);
        }
        public BrowserProfile Profile { get; }
        public BotInfo Bot { get; }

        public event EventHandler? ConstructorCompleted;

        #region Commands

        public ICommand StartConstructorBot
        {
            get => field ??= new RelayCommand(StartConstructorBotExecuteAsync);
        }

        #endregion 

        public BotConstructorViewModel(IBotConstructor constructor, BrowserProfile profile, BotInfo bot)
        {
            _constructor = constructor;
            Profile = profile;
            Bot = bot;
            _constructor.BrowserEventReceived += AdddBrowserEvent;
            _constructor.ConstructionCompleted += Constructor_ConstructionCompleted;
        }

        private void Constructor_ConstructionCompleted(object? sender, EventArgs e)
        {
            OnConstructorCompleted();
        }

        private void AdddBrowserEvent(object? sender, BrowserEvent e)
        {
            Bot.BrowserEvents.Add(e);
        }

        private void OnConstructorCompleted()
        {
            ConstructorCompleted?.Invoke(this, EventArgs.Empty);
        }

        #region Method for commands

        private async void StartConstructorBotExecuteAsync()
        {
            await _constructor.StartConstructorAsync(Profile, Bot).ConfigureAwait(true);
        }

        #endregion
    }
}
