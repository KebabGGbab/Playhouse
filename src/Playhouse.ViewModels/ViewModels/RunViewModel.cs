using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Playhouse.Core.Services.BotRunningService;
using Playhouse.ViewModels.Messages;

namespace Playhouse.ViewModels.ViewModels
{
    public class RunViewModel : ObservableObject
    {
        private readonly IRunServiceFactory _runServiceFactory;

        private readonly ObservableCollection<BrowserConfigurationViewModel> _unselectedProfiles = [];
        private readonly ObservableCollection<BrowserConfigurationViewModel> _selectedProfiles = [];
        private readonly ObservableCollection<BotConfigurationViewModel> _bots = [];
        private readonly ObservableCollection<RunServiceViewModel> _runningTasks = [];
        private readonly Dictionary<RunServiceViewModel, BrowserConfigurationViewModel[]> _busyProfiles = [];

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> UnselectedProfiles { get; }

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> SelectedProfiles { get; }

        public ReadOnlyObservableCollection<BotConfigurationViewModel> Bots { get; }

        public ReadOnlyObservableCollection<RunServiceViewModel> RunningTasks { get; }

		public BotConfigurationViewModel? SelectedBotStart
		{
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    RunBotCommand.NotifyCanExecuteChanged();
                }
            }
		}

		public IRelayCommand<BrowserConfigurationViewModel> SelectProfileCommand { get; }

        public IRelayCommand<BrowserConfigurationViewModel> ExcludeProfileCommand { get; }

        public IRelayCommand RunBotCommand { get; }

        public RunViewModel(IRunServiceFactory runServiceFactory)
        {
            _runServiceFactory = runServiceFactory;
            UnselectedProfiles = new(_unselectedProfiles);
            SelectedProfiles = new(_selectedProfiles);
            Bots = new(_bots);
            RunningTasks = new(_runningTasks);
            WeakReferenceMessenger.Default.Register<RunViewModel, CollectionChangedMessage<BrowserConfigurationViewModel>>(this, OnSourceProfilesCollectionChanged);
            WeakReferenceMessenger.Default.Register<RunViewModel, CollectionChangedMessage<BotConfigurationViewModel>>(this, OnSourceBotsCollectionChanged);
            SelectProfileCommand = new RelayCommand<BrowserConfigurationViewModel>(SelectProfile);
            ExcludeProfileCommand = new RelayCommand<BrowserConfigurationViewModel>(ExcludeProfile);
            RunBotCommand = new RelayCommand(ExecuteRunBot, CanExecuteRunBot);
        }

        private static void OnSourceProfilesCollectionChanged(RunViewModel recipient, CollectionChangedMessage<BrowserConfigurationViewModel> e)
        {
            switch (e.Action)
            {
                case CollectionChangedAction.Add:
                    recipient._unselectedProfiles.Add(e.Items);
                    break;
                case CollectionChangedAction.Remove:
                    foreach (BrowserConfigurationViewModel vm in e.Items)
                    {
                        if (!recipient._unselectedProfiles.Remove(vm))
                            recipient._selectedProfiles.Remove(vm);
                    }
                    break;
            }
        }

        private static void OnSourceBotsCollectionChanged(RunViewModel recipient, CollectionChangedMessage<BotConfigurationViewModel> e)
        {
            switch (e.Action)
            {
                case CollectionChangedAction.Add:
                    recipient._bots.Add(e.Items);
                    break;
                case CollectionChangedAction.Remove:
                    recipient._bots.Remove(e.Items);
                    break;
            }
        }

        private void SelectProfile(BrowserConfigurationViewModel? arg)
        {
            ArgumentNullException.ThrowIfNull(arg);

			_unselectedProfiles.Remove(arg);
			_selectedProfiles.Add(arg);
            RunBotCommand.NotifyCanExecuteChanged();
        }

        private void ExcludeProfile(BrowserConfigurationViewModel? arg)
        {
            ArgumentNullException.ThrowIfNull(arg);

            _selectedProfiles.Remove(arg);
			_unselectedProfiles.Add(arg);
            RunBotCommand.NotifyCanExecuteChanged();
        }

        private async void ExecuteRunBot()
        {
            if (!CanExecuteRunBot())
            {
                return;
            }

            IRunService runService = _runServiceFactory.Create(SelectedBotStart.Bot, _selectedProfiles.Select(vm => vm.Profile).ToList());
            RunServiceViewModel runVM = new(runService, SelectedBotStart, _selectedProfiles);
            _runningTasks.Add(runVM);
            _busyProfiles.Add(runVM, _selectedProfiles.ToArray());
            _selectedProfiles.Clear();
            runVM.RunCompleted += OnRunServiceCompleted;
            await runVM.RunAsync().ConfigureAwait(false);
        }

        [MemberNotNullWhen(true, nameof(SelectedBotStart))]
        private bool CanExecuteRunBot()
        {
            return SelectedBotStart != null && _selectedProfiles.Count > 0;
        }

        private void OnRunServiceCompleted(RunServiceViewModel sender, EventArgs e)
        {
            sender.RunCompleted -= OnRunServiceCompleted;
            _unselectedProfiles.Add(_busyProfiles[sender]);
            _busyProfiles.Remove(sender);
        }
    }
}
