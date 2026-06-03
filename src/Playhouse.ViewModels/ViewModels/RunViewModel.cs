using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Playhouse.Core.Services.BotRunningService;
using Playhouse.Core.Services.BotRunningService.Abstrtactions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.ViewModels.Messages;

namespace Playhouse.ViewModels.ViewModels
{
    public class RunViewModel : ObservableObject
    {
        private readonly IBotJobManagerFactory _jobManagerFactory;
        private readonly IFilePathResolver _filePathResolver;

        private readonly ObservableCollection<BrowserConfigurationViewModel> _unselectedProfiles = [];
        private readonly ObservableCollection<BrowserConfigurationViewModel> _selectedProfiles = [];
        private readonly ObservableCollection<BotConfigurationViewModel> _bots = [];
        private readonly ObservableCollection<BotJobManager> _runningTasks = [];
        private readonly Dictionary<BotJobManager, BrowserConfigurationViewModel[]> _busyProfiles = [];

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> UnselectedProfiles { get; }

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> SelectedProfiles { get; }

        public ReadOnlyObservableCollection<BotConfigurationViewModel> Bots { get; }

        public ReadOnlyObservableCollection<BotJobManager> RunningTasks { get; }

		public BotConfigurationViewModel? SelectedBotStart
		{
			get => field;
            set => SetProperty(ref field, value);
		}

		public ICommand SelectProfileCommand => field ??= new RelayCommand<BrowserConfigurationViewModel>(ExecuteSelectProfile);

        public ICommand ExcludeProfileCommand => field ??= new RelayCommand<BrowserConfigurationViewModel>(ExecuteExcludeProfile);

        public ICommand RunBotCommand => field ??= new RelayCommand(ExecuteRunBot, CanExecuteRunBot);

        public RunViewModel(IBotJobManagerFactory botJobManagerFactory, IFilePathResolver pathResolver)
        {
            _jobManagerFactory = botJobManagerFactory;
            _filePathResolver = pathResolver;
            UnselectedProfiles = new(_unselectedProfiles);
            SelectedProfiles = new(_selectedProfiles);
            Bots = new(_bots);
            RunningTasks = new(_runningTasks);
            WeakReferenceMessenger.Default.Register<RunViewModel, CollectionChangedMessage<BrowserConfigurationViewModel>>(this, OnSourceProfilesCollectionChanged);
            WeakReferenceMessenger.Default.Register<RunViewModel, CollectionChangedMessage<BotConfigurationViewModel>>(this, OnSourceBotsCollectionChanged);
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

        private void ExecuteSelectProfile(BrowserConfigurationViewModel? arg)
        {
            ArgumentNullException.ThrowIfNull(arg, nameof(arg));

			_unselectedProfiles.Remove(arg);
			_selectedProfiles.Add(arg);
        }

        private void ExecuteExcludeProfile(BrowserConfigurationViewModel? arg)
        {
            ArgumentNullException.ThrowIfNull(arg, nameof(arg));

            _selectedProfiles.Remove(arg);
			_unselectedProfiles.Add(arg);
        }

        private bool CanExecuteRunBot()
        {
            return SelectedBotStart != null && _selectedProfiles.Count > 0;
        }

        private async void ExecuteRunBot()
        {
            BotJobManager jobManager = _jobManagerFactory.Create(new BotJobContext(_selectedProfiles.Select(vm => vm.Profile).ToList(), SelectedBotStart!.Bot));
            _runningTasks.Add(jobManager);
            _busyProfiles.Add(jobManager, _selectedProfiles.ToArray());
            _selectedProfiles.Clear();
            jobManager.Completed += OnJobManager_Completed;
            await jobManager.ExecuteJobsAsync(new BotManagerRunArgs(_filePathResolver.GetBotDllFile(SelectedBotStart!.Bot.Id).FullName)).ConfigureAwait(false);
        }

        private void OnJobManager_Completed(object? sender, EventArgs e)
        {
            BotJobManager jobManager = (BotJobManager)sender!;
            jobManager.Completed -= OnJobManager_Completed;
            _unselectedProfiles.Add(_busyProfiles[jobManager]);
            _busyProfiles.Remove(jobManager);
        }
    }
}
