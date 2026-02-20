using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.ViewModels.Messages;
using Playhouse.ViewModels.ViewModelsExtensions;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Playhouse.ViewModels.ViewModels
{
	public class BotsInfoViewModel : BaseCollectionViewModel<BotInfoViewModel>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly SourceCache<BotInfoViewModel, int> _botsSource = new(b => b.Id);
        private ReadOnlyObservableCollection<BotInfoViewModel> _bots;
        public ReadOnlyObservableCollection<BotInfoViewModel> Bots => _bots;

        #region Property for create bot

        private readonly SourceCache<BrowserProfileViewModel, int> _profilesSource = new(p => p.Id);
        private ReadOnlyObservableCollection<BrowserProfileViewModel> _profiles;

        public ReadOnlyObservableCollection<BrowserProfileViewModel> Profiles => _profiles;

		public string ProfileNameFilterForCreateBot
        {
            get => field;
            set => SetProperty(ref field, value);
        } = string.Empty;

        public BrowserProfileViewModel? SelectedProfileCreate
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

        public BrowserType? BrowserType
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
        private ReadOnlyObservableCollection<BotInfoViewModel> _botsDelete;

        public ReadOnlyObservableCollection<BotInfoViewModel> BotsDelete => _botsDelete;
		public BotInfoViewModel? SelectedBotDelete
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

		public IRelayCommand CreateBotCommand => field ??= new AsyncRelayCommand(CreateBot, CanCreateBot);
        public IRelayCommand DeleteBotCommand => field ??= new AsyncRelayCommand(ExecuteDeleteBot, CanExecuteDeleteBot);
        public IRelayCommand SaveBotCommand => field ??= new AsyncRelayCommand<BotInfoViewModel>(SaveBot);

		public BotsInfoViewModel(IDbContextFactory<ApplicationDbContext> dbContextFactory)
		{
            _dbContextFactory = dbContextFactory;
            FillSource(LoadBotsAsync().Result);
            WeakReferenceMessenger.Default.Register<BotsInfoViewModel, CollectionChangedMessage<BrowserProfileViewModel>>(this, OnSourceProfilesCollectionChanged);
            _profilesSource.Connect()
                .Filter(p => p.FilterByName(ProfileNameFilterForCreateBot))
                .Bind(out _profiles)
                .Subscribe();
        }

        private async Task<List<BotInfoViewModel>> LoadBotsAsync()
        {
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();

            return await dbContext.BotsInfo
                .Include(b => b.BrowserEvents)
                .ToAsyncEnumerable()
                .Select(b => new BotInfoViewModel(b))
                .ToListAsync();
        }

        [MemberNotNull(nameof(_botsDelete), nameof(_bots))]
        private void FillSource(List<BotInfoViewModel> bots)
        {
            _botsSource.Remove(_botsSource.Items);
            _botsSource.AddOrUpdate(bots);

            _botsSource.Connect()
                .Filter(b => b.FilterByName(BotNameFilterForDelete))
                .Bind(out _botsDelete)
                .Subscribe();
            _botsSource.Connect()
                .Bind(out _bots)
                .Subscribe();

            SendMessageAddItems(bots);
        }

        private void OnSourceProfilesCollectionChanged(BotsInfoViewModel recipient, CollectionChangedMessage<BrowserProfileViewModel> e)
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

            BotInfoViewModel newBot = new(new BotInfo()
            {
                Browser = (BrowserType)BrowserType,
                Name = BotNameCreate,
            });
            BrowserType = null;
            BotNameCreate = string.Empty;
            await WeakReferenceMessenger.Default.Send(new GetBrowserEventsMessage(newBot, SelectedProfileCreate));
            await SaveBot(newBot);
		}

		private bool CanCreateBot() => SelectedProfileCreate != null  && BrowserType != null;

        private async Task ExecuteDeleteBot()
        {
            if (SelectedBotDelete == null)
            {
                return;
            }

            BotInfoViewModel bot = SelectedBotDelete;
			_botsSource.Remove(bot);
            SelectedBotDelete = null;
            IsConfirmDelete = false;
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
            dbContext.BotsInfo.Remove(bot.Bot);
            await dbContext.SaveChangesAsync();
            SendMessageRemoveItems([bot]);
		}

        private bool CanExecuteDeleteBot() => IsConfirmDelete && _botsSource.Items.Contains(SelectedBotDelete);

        private async Task SaveBot(BotInfoViewModel? bot)
        {
            if (bot == null)
            {
                return; 
            }

            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
            await dbContext.BotsInfo.AddAsync(bot.Bot);
            await dbContext.SaveChangesAsync();
            _botsSource.AddOrUpdate(bot);
            SendMessageAddItems([bot]);
        }
	}
}
