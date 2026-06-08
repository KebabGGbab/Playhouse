using System.Collections.ObjectModel;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.ViewModels.ViewModels.BotActionViewModels;
using Playhouse.ViewModels.Visitor;
using ReactiveUI;

namespace Playhouse.ViewModels.ViewModels
{
    public class BotConfigurationViewModel : ObservableObject
    {
        private readonly SourceList<BotActionViewModel> _actionSource;
        private readonly ReadOnlyObservableCollection<BotActionViewModel> _actions;
        private readonly ViewModelsBrowserEventVisitor _visitor = new();

        internal BotConfiguration Bot { get; }

        public int Id => Bot.Id;

        public string Name => Bot.Name;

        public BrowserTypes Browser => Bot.Browser;

        public ReadOnlyObservableCollection<BotActionViewModel> Actions => _actions;

        public IRelayCommand SaveCommand { get; }

        public BotConfigurationViewModel()
            : this(new BotConfiguration(BrowserTypes.Chromium))
        {
        }

        public BotConfigurationViewModel(BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(bot);

            Bot = bot;
            _actionSource = new SourceList<BotActionViewModel>();
            _actionSource.Connect()
                .ObserveOn(RxSchedulers.MainThreadScheduler)
                .Bind(out _actions)
                .Subscribe();

            foreach (BotAction action in Bot.Actions)
            {
                AddAction2(action);
            }
            SaveCommand = new RelayCommand(Save);
        }

        internal void AddAction(BotAction action)
        {
            Bot.Actions.Add(action);
            AddAction2(action);
        }

        private void AddAction2(BotAction action)
        {
            _actionSource.Add(action.Accept(_visitor));
        }

        private void Save()
        {
            foreach (BotActionViewModel action in _actions)
            {
                action.SaveChangesCommand.Execute(null);
            }
        }
    }
}