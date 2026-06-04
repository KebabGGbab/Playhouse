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

namespace Playhouse.ViewModels.ViewModels
{
	public class BotConfigurationsViewModel : BaseCollectionViewModel<BotConfigurationViewModel>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IViewModelFactory<BotConfigurationViewModel, BotConfiguration> _viewModelFactory;
        private readonly SourceCache<BotConfigurationViewModel, int> _botsSource = new(b => b.Id);
        private ReadOnlyObservableCollection<BotConfigurationViewModel> _bots;
        public ReadOnlyObservableCollection<BotConfigurationViewModel> Bots => _bots;

        #region Property for create bot

        private readonly SourceCache<BrowserConfigurationViewModel, int> _profilesSource = new(p => p.Id);
        private ReadOnlyObservableCollection<BrowserConfigurationViewModel> _profiles;

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> Profiles => _profiles;

		public string ProfileNameFilterForCreateBot
        {
            get => field;
            set => SetProperty(ref field, value);
        } = string.Empty;

        public BrowserConfigurationViewModel? SelectedProfileCreate
        {
            get => field;
            set => SetProperty(field, value, p =>
            {
                field = p;
                CreateBotCommand.NotifyCanExecuteChanged();
            });
        }

        public string BotNameCreate
        {
            get => field;
            set => SetProperty(field, value, n =>
            {
                field = n;
                CreateBotCommand.NotifyCanExecuteChanged();
            });
        } = string.Empty;

        public BrowserTypes? BrowserType
        {
            get => field;
            set => SetProperty(field, value, t =>
            {
                field = t;
                CreateBotCommand.NotifyCanExecuteChanged();
            });
        }

        #endregion

        #region Property for delete bot
        private ReadOnlyObservableCollection<BotConfigurationViewModel> _botsDelete;

        public ReadOnlyObservableCollection<BotConfigurationViewModel> BotsDelete => _botsDelete;
		public BotConfigurationViewModel? SelectedBotDelete
        {
            get => field;
            set => SetProperty(ref field, value);
        }
        public string BotNameFilterForDelete { get; set; } = string.Empty;
        public bool IsConfirmDelete
        {
            get => field;
            set => SetProperty(ref field, value);
        }

        #endregion

        public IAsyncRelayCommand LoadDataCommand => field ??= new AsyncRelayCommand(LoadDataAsync);

		public IRelayCommand CreateBotCommand => field ??= new AsyncRelayCommand(CreateBot, CanCreateBot);

        public IRelayCommand DeleteBotCommand => field ??= new AsyncRelayCommand(ExecuteDeleteBot, CanExecuteDeleteBot);

        public IRelayCommand SaveBotCommand => field ??= new AsyncRelayCommand<BotConfigurationViewModel>(SaveBot);

		public BotConfigurationsViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, IViewModelFactory<BotConfigurationViewModel, BotConfiguration> viewModelFactory)
		{
            _dbFactory = dbFactory;
            _viewModelFactory = viewModelFactory;

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
            if (SelectedProfileCreate == null || BrowserType == null)
            {
                return;
            }

            BotConfigurationViewModel newBot = _viewModelFactory.Create(new BotConfiguration(BrowserType)
            {
                Name = BotNameCreate,
            });
            BrowserType = null;
            BotNameCreate = string.Empty;
            await WeakReferenceMessenger.Default.Send(new GetBotActionsMessage(newBot, SelectedProfileCreate));
            await SaveBot(newBot);
		}

		private bool CanCreateBot() => SelectedProfileCreate != null  && BrowserType != null;

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
