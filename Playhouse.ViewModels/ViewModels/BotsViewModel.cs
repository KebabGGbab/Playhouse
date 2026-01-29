using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Services.EntityManagerService.Abstractions;
using Playhouse.ViewModels.Messages;
using Playhouse.ViewModels.ModelsExtensions;
using System.Collections.ObjectModel;

namespace Playhouse.ViewModels.ViewModels
{
	public sealed class BotViewModel : BaseCollectionViewModel<BotInfo>
    {
        private readonly IEntityManager<BotInfo> _botManager;

        private readonly SourceCache<BotInfo, int> _botsSource = new(b => b.Id);
        private ReadOnlyObservableCollection<BotInfo> _bots;
        public ReadOnlyObservableCollection<BotInfo> Bots => _bots;

        #region Property for create bot

        private readonly SourceCache<BrowserProfile, int> _profilesSource = new(p => p.Id);
        private readonly ReadOnlyObservableCollection<BrowserProfile> _profiles;

        public ReadOnlyObservableCollection<BrowserProfile> Profiles => _profiles;

		public string ProfileNameFilterForCreateBot
        {
            get => field;
            set => SetProperty(ref field, value);
        } = string.Empty;

        public BrowserProfile? SelectedProfileCreate
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
        private readonly ReadOnlyObservableCollection<BotInfo> _botsDelete;

        public ReadOnlyObservableCollection<BotInfo> BotsDelete => _botsDelete;
		public BotInfo? SelectedBotDelete
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

        #region Commands

		public IRelayCommand CreateBotCommand => field ??= new RelayCommand<BotInfo>(ExecuteCreateBot, CanExecuteCreateBot);
        public IRelayCommand DeleteBotCommand => field ??= new RelayCommand(ExecuteDeleteBot, CanExecuteDeleteBot);

        #endregion

		public BotViewModel(IEntityManager<BotInfo> botManager)
		{
            ArgumentNullException.ThrowIfNull(botManager, nameof(botManager));

			_botManager = botManager;
            LoadBotsAsync();
            _botsSource.Connect()
                .Filter(b => b.FilterByName(BotNameFilterForDelete))
                .Bind(out _botsDelete)
                .Subscribe();
            _botsSource.Connect()
                .Bind(out _bots)
                .Subscribe();
            _profilesSource.Connect()
                .Filter(p => p.FilterByName(ProfileNameFilterForCreateBot))
                .Bind(out _profiles)
                .Subscribe();
            WeakReferenceMessenger.Default.Register<BotViewModel, CollectionChangeMessage<BrowserProfile>>(this, OnSourceProfilesCollectionChanged);
        }

        private async void LoadBotsAsync()
        {
            _botsSource.AddOrUpdate(await _botManager.GetAsync().ConfigureAwait(true));

            foreach (BotInfo bot in _botsSource.Items)
            {
                SendMessageAddItem(bot);
            }
        }

        private void OnSourceProfilesCollectionChanged(BotViewModel recipient, CollectionChangeMessage<BrowserProfile> e)
        {
            switch (e.Action)
            {
                case CollectionChangeAction.Add: 
                    _profilesSource.AddOrUpdate(e.Item);
                    break;
                case CollectionChangeAction.Remove:
                    _profilesSource.Remove(e.Item);
                    break;
            }
        }

        #region Methods for command

        private async void ExecuteCreateBot(BotInfo? arg)
        {
            if (arg == null)
            {
                return;
            }

            await _botManager.CreateOrUpdate(arg).ConfigureAwait(true);
			_botsSource.AddOrUpdate(arg);
            SendMessageAddItem(arg);
		}

		private bool CanExecuteCreateBot(BotInfo? arg)
        {
            return arg != null && SelectedProfileCreate != null
                && !string.IsNullOrWhiteSpace(BotNameCreate)
                && BrowserType != null;
		}

        private void ExecuteDeleteBot()
        {
			if (SelectedBotDelete != null)
			{
				_botsSource.Remove(SelectedBotDelete);
                SendMessageRemoveItem(SelectedBotDelete);
                _botManager.Delete(SelectedBotDelete.Id);
                IsConfirmDelete = false;
                SelectedBotDelete = null;
            }
		}

        private bool CanExecuteDeleteBot()
        {
            return IsConfirmDelete 
                && _botsSource.Items.Contains(SelectedBotDelete);
		}

        #endregion
	}
}
