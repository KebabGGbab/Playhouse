using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.ViewModels.ViewModels.EventBrowserViewModels;
using Playhouse.ViewModels.Visitor;

namespace Playhouse.ViewModels.ViewModels
{
    public class BotInfoViewModel : ObservableObject
    {
        private readonly ObservableCollection<BrowserEventViewModel> _browserEvents = [];
        private readonly ViewModelsBrowserEventVisitor _visitor = new();

        internal BotInfo Bot { get; }

        public int Id => Bot.Id;

        public string Name => Bot.Name;

        public BrowserType Browser => Bot.Browser;

        public ReadOnlyObservableCollection<BrowserEventViewModel> BrowserEvents { get; }

        public BotInfoViewModel() : this(new BotInfo()) 
        {
        }

        public BotInfoViewModel(BotInfo bot)
        {
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));

            Bot = bot;
            BrowserEvents = new ReadOnlyObservableCollection<BrowserEventViewModel>(_browserEvents);

            foreach (BrowserEvent @event in Bot.BrowserEvents)
            {
                AddEvents(@event);
            }
        }

        internal void AddEvents(BrowserEvent @event)
        {
            @event.Accept(_visitor);
            _browserEvents.Add(_visitor.CurrentViewModel!);
        } 
    }
}