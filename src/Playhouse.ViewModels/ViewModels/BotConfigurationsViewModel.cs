using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using Playhouse.Domain;
using Playhouse.Infrastructure;
using Playhouse.ViewModels.Messages;
using Playhouse.ViewModels.Services.BrowserConfigurationViewModelService;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels.Abstractions;
using Playhouse.ViewModels.ViewModelsExtensions;

namespace Playhouse.ViewModels.ViewModels
{
	public class BotConfigurationsViewModel : BaseCollectionViewModel<BotConfigurationViewModel>, IDisposable
    {
        // Зависимости
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IViewModelFactory<BotConfigurationViewModel, BotConfiguration> _viewModelFactory;
        private readonly IBrowserConfigurationViewModelService _browserConfigurationViewModelService;

        private readonly SourceCache<BotConfigurationViewModel, int> _botsSource = new(b => b.Id);
        private readonly ReadOnlyObservableCollection<BotConfigurationViewModel> _bots;

        private bool _disposed;

        public ReadOnlyObservableCollection<BotConfigurationViewModel> Bots => _bots;

        #region Property for create bot

        private readonly SourceList<BrowserConfigurationViewModel> _profilesSource;
        private readonly IDisposable _bindingBrowserConfigurationSource;
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
            set
            {
                if (SetProperty(ref field, value))
                {
                    DeleteBotCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public string BotNameFilterForDelete 
        {
            get => _botNameFilterDelete;
            set => SetProperty(ref _botNameFilterDelete, value);
        }

        public bool IsConfirmDelete
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    DeleteBotCommand.NotifyCanExecuteChanged();
                }
            }
        }

        #endregion

        public IAsyncRelayCommand LoadDataCommand { get; }

		public IAsyncRelayCommand CreateBotCommand { get; }

        public IAsyncRelayCommand DeleteBotCommand { get; }

        public IAsyncRelayCommand<BotConfigurationViewModel> SaveBotCommand { get; }

        public BotConfigurationsViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, IViewModelFactory<BotConfigurationViewModel, BotConfiguration> viewModelFactory, IBrowserConfigurationViewModelService browserConfigurationViewModelService)
		{
            ArgumentNullException.ThrowIfNull(browserConfigurationViewModelService);

            _dbFactory = dbFactory;
            _viewModelFactory = viewModelFactory;
            _browserConfigurationViewModelService = browserConfigurationViewModelService;
            Browsers = BrowserTypes.List;
            _botNameCreate = string.Empty;
            _profileNameFilterCreate = string.Empty;
            _botNameFilterDelete = string.Empty;
            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            CreateBotCommand = new AsyncRelayCommand(CreateBot, CanCreateBot);
            DeleteBotCommand = new AsyncRelayCommand(ExecuteDeleteBot, CanExecuteDeleteBot);
            SaveBotCommand = new AsyncRelayCommand<BotConfigurationViewModel>(SaveBot);
            _profilesSource = new();
            _bindingBrowserConfigurationSource = _browserConfigurationViewModelService.Configurations
                .ToObservableChangeSet()
                .PopulateInto(_profilesSource);
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
                .Include(b => b.Actions.OrderBy(i => i.ActionNumber))
                .Select(b => _viewModelFactory.Create(b))
                .ToListAsync();

            IReadOnlyList<BotConfigurationViewModel> oldBots = _botsSource.Items;
            _botsSource.Remove(oldBots);
            _botsSource.AddOrUpdate(bots);

            SendMessageRemoveItems(oldBots);
            SendMessageAddItems(bots);
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
            if (!CanExecuteDeleteBot())
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

        [MemberNotNullWhen(true, nameof(SelectedBotDelete))]
        private bool CanExecuteDeleteBot() => IsConfirmDelete && SelectedBotDelete != null && _botsSource.Items.Contains(SelectedBotDelete);

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

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            GC.SuppressFinalize(this);
            _bindingBrowserConfigurationSource.Dispose();
        }
	}
}
