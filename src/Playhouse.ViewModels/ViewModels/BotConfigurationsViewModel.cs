using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.ViewModels.Messages;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels.Abstractions;
using Playhouse.ViewModels.ViewModelsExtensions;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Playhouse.ViewModels.ViewModels
{
	public class BotConfigurationsViewModel : BaseCollectionViewModel<BotConfigurationViewModel>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IViewModelFactory<BotConfigurationViewModel, BotConfiguration> _viewModelFactory;
        private readonly SourceCache<BotConfigurationViewModel, int> _botsSource = new(b => b.Id);
        private readonly ReadOnlyObservableCollection<BotConfigurationViewModel> _bots;
        public ReadOnlyObservableCollection<BotConfigurationViewModel> Bots => _bots;

        #region Property for create bot

        private readonly SourceCache<BrowserConfigurationViewModel, int> _profilesSource = new(p => p.Id);
        private readonly ReadOnlyObservableCollection<BrowserConfigurationViewModel> _profiles;

        private string _profileNameFilterCreate;
        private string _botNameCreate;

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> Profiles => _profiles;

		public string ProfileNameFilterForCreateBot
        {
            get => _profileNameFilterCreate;
            set
            {
                if (SetProperty(ref _profileNameFilterCreate, value))
                {
                    CreateBotCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public BrowserConfigurationViewModel? SelectedProfileCreate
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CreateBotCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public string BotNameCreate
        {
            get => _botNameCreate;
            set
            {
                if (SetProperty(ref _botNameCreate, value))
                {
                    CreateBotCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IReadOnlyCollection<BrowserTypes> Browsers { get; } = BrowserTypes.List;

        public BrowserTypes? SelectedBrowserType
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CreateBotCommand.NotifyCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Property for delete bot
        private readonly ReadOnlyObservableCollection<BotConfigurationViewModel> _botsDelete;

        private string _botNameFilterDelete;

        public ReadOnlyObservableCollection<BotConfigurationViewModel> BotsDelete => _botsDelete;

		public BotConfigurationViewModel? SelectedBotDelete
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string BotNameFilterForDelete 
        {
            get => _botNameFilterDelete;
            set => SetProperty(ref _botNameFilterDelete, value);
        }

        public bool IsConfirmDelete
        {
            get;
            set => SetProperty(ref field, value);
        }

        #endregion

        public IAsyncRelayCommand LoadDataCommand { get; }

		public IAsyncRelayCommand CreateBotCommand { get; }

        public IAsyncRelayCommand DeleteBotCommand { get; }

        public IAsyncRelayCommand<BotConfigurationViewModel> SaveBotCommand { get; }

        public BotConfigurationsViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, IViewModelFactory<BotConfigurationViewModel, BotConfiguration> viewModelFactory)
		{
            _dbFactory = dbFactory;
            _viewModelFactory = viewModelFactory;
            Browsers = BrowserTypes.List;
            _botNameCreate = string.Empty;
            _profileNameFilterCreate = string.Empty;
            _botNameFilterDelete = string.Empty;
            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            CreateBotCommand = new AsyncRelayCommand(CreateBot, CanCreateBot);
            DeleteBotCommand = new AsyncRelayCommand(ExecuteDeleteBot, CanExecuteDeleteBot);
            SaveBotCommand = new AsyncRelayCommand<BotConfigurationViewModel>(SaveBot);
            WeakReferenceMessenger.Default.Register<BotConfigurationsViewModel, CollectionChangedMessage<BrowserConfigurationViewModel>>(this, OnSourceProfilesCollectionChanged);
            _profilesSource.Connect()
                .Filter(p => p.FilterByName(ProfileNameFilterForCreateBot))
                .Bind(out _profiles)
                .Subscribe();
            _botsSource.Connect()
                .Filter(b => b.FilterByName(BotNameFilterForDelete))
                .Bind(out _botsDelete)
                .Subscribe();
            _botsSource.Connect()
                .Bind(out _bots)
                .Subscribe();
        }

        private async Task LoadDataAsync()
        {
            using ApplicationDbContext dbContext = await _dbFactory.CreateDbContextAsync();

            List<BotConfigurationViewModel> bots = await dbContext.Bots
                .Include(b => b.Actions)
                .Select(b => _viewModelFactory.Create(b))
                .ToListAsync();

            IReadOnlyList<BotConfigurationViewModel> oldBots = _botsSource.Items;
            _botsSource.Remove(oldBots);
            _botsSource.AddOrUpdate(bots);

            SendMessageRemoveItems(oldBots);
            SendMessageAddItems(bots);
        }

        private void OnSourceProfilesCollectionChanged(BotConfigurationsViewModel recipient, CollectionChangedMessage<BrowserConfigurationViewModel> e)
        {
            switch (e.Action)
            {
                case CollectionChangedAction.Add: 
                    _profilesSource.AddOrUpdate(e.Items);
                    break;
                case CollectionChangedAction.Remove:
                    _profilesSource.Remove(e.Items);
                    break;
            }
        }

        private async Task CreateBot()
        {
            if (!CanCreateBot())
            {
                return;
            }

            BotConfigurationViewModel newBot = _viewModelFactory.Create(new BotConfiguration(SelectedBrowserType)
            {
                Name = BotNameCreate,
            });
            SelectedBrowserType = null;
            BotNameCreate = string.Empty;
            newBot = await WeakReferenceMessenger.Default.Send(new GetBotActionsMessage(newBot, SelectedProfileCreate));
            await SaveBot(newBot);
		}


        [MemberNotNullWhen(true, nameof(SelectedProfileCreate), nameof(SelectedBrowserType))]
		private bool CanCreateBot() => !(SelectedProfileCreate == null || SelectedBrowserType == null);

        private async Task ExecuteDeleteBot()
        {
            if (SelectedBotDelete == null)
            {
                return;
            }

            BotConfigurationViewModel bot = SelectedBotDelete;
			_botsSource.Remove(bot);
            SelectedBotDelete = null;
            IsConfirmDelete = false;
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();
            db.Bots.Remove(bot.Bot);
            await db.SaveChangesAsync();
            SendMessageRemoveItems([bot]);
		}

        private bool CanExecuteDeleteBot() => IsConfirmDelete && _botsSource.Items.Contains(SelectedBotDelete);

        private async Task SaveBot(BotConfigurationViewModel? bot)
        {
            if (bot == null)
            {
                return; 
            }

            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();
            await db.Bots.AddAsync(bot.Bot);
            await db.SaveChangesAsync();
            _botsSource.AddOrUpdate(bot);
            SendMessageAddItems([bot]);
        }
	}
}
