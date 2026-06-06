using System.Collections.ObjectModel;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.ViewModels.ViewModels.BotActionViewModels;
using Playhouse.ViewModels.Visitor;
using ReactiveUI;

namespace Playhouse.ViewModels.ViewModels
{
    public class BotConfigurationViewModel : ObservableObject
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly FileManager<BotConfiguration> _fileManager;

        private readonly SourceCache<BotActionViewModel, int> _actionSource;
        private readonly ReadOnlyObservableCollection<BotActionViewModel> _actions;
        private readonly ViewModelsBrowserEventVisitor _visitor = new();

        internal BotConfiguration Bot { get; }

        public int Id => Bot.Id;

        public string Name => Bot.Name;

        public BrowserTypes Browser => Bot.Browser;

        public ReadOnlyObservableCollection<BotActionViewModel> Actions => _actions;

        public BotConfigurationViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, FileManager<BotConfiguration> fileManager)
            : this(dbFactory, fileManager, new BotConfiguration(BrowserTypes.Chromium))
        {
        }

        public BotConfigurationViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, FileManager<BotConfiguration> fileManager, BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));
            ArgumentNullException.ThrowIfNull(dbFactory, nameof(dbFactory));
            ArgumentNullException.ThrowIfNull(fileManager, nameof(fileManager));

            _dbFactory = dbFactory;
            _fileManager = fileManager;
            Bot = bot;
            _actionSource = new SourceCache<BotActionViewModel, int>((b) => b.Id);
            _actionSource.Connect()
                .ObserveOn(RxSchedulers.MainThreadScheduler)
                .Bind(out _actions)
                .Subscribe();

            foreach (BotAction @event in Bot.Actions)
            {
                AddAction(@event);
            }
        }

        internal void AddAction(BotAction @event)
        {
            @event.Accept(_visitor);
            _actionSource.AddOrUpdate(_visitor.CurrentViewModel!);
        }
    }
}