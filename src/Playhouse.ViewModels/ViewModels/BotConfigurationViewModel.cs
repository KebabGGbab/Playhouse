using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.ViewModels.ViewModels.BotActionViewModels;
using Playhouse.ViewModels.Visitor;

namespace Playhouse.ViewModels.ViewModels
{
    public class BotConfigurationViewModel : ObservableObject
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly FileManager<BotConfiguration> _fileManager;

        private readonly ObservableCollection<BotActionViewModel> _actions = [];
        private readonly ViewModelsBrowserEventVisitor _visitor = new();

        internal BotConfiguration Bot { get; }

        public int Id => Bot.Id;

        public string Name => Bot.Name;

        public BrowserType Browser => Bot.Browser;

        public ReadOnlyObservableCollection<BotActionViewModel> Actions { get; }

        public BotConfigurationViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, FileManager<BotConfiguration> fileManager)
            : this(dbFactory, fileManager, new BotConfiguration())
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
            Actions = new ReadOnlyObservableCollection<BotActionViewModel>(_actions);

            foreach (BotAction @event in Bot.Actions)
            {
                AddAction(@event);
            }
        }

        internal void AddAction(BotAction @event)
        {
            @event.Accept(_visitor);
            _actions.Add(_visitor.CurrentViewModel!);
        }
    }
}