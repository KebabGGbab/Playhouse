using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Playhouse.Core.Models;
using Playhouse.Core.Services.BotRunningService;
using Playhouse.Core.Services.BotRunningService.Abstrtactions;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.ViewModels.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Playhouse.ViewModels.ViewModels
{
    public class RunViewModel : ObservableObject
    {
        private readonly IBotJobManagerFactory _jobManagerFactory;
        private readonly IFilePathResolver _filePathResolver;

        public ObservableCollection<BotJobManager> RunningTasks { get; } = [];
        public ObservableCollection<BrowserProfile> UnselectedProfiles { get; } = [];
        public ObservableCollection<BrowserProfile> SelectedProfiles { get; } = [];
        public ObservableCollection<BotInfo> Bots { get; } = [];

		public BotInfo? SelectedBotStart
		{
			get => field;
            set => SetProperty(ref field, value);
		}

		#region Commands

		public ICommand SelectProfileCommand => field ??= new RelayCommand<BrowserProfile>(ExecuteSelectProfile);
        public ICommand ExcludeProfileCommand => field ??= new RelayCommand<BrowserProfile>(ExecuteExcludeProfile);
        public ICommand RunBotCommand => field ??= new RelayCommand(ExecuteRunBot, CanExecuteRunBot);

        #endregion

        public RunViewModel(IBotJobManagerFactory botJobManagerFactory, IFilePathResolver pathResolver)
        {
            _jobManagerFactory = botJobManagerFactory;
            _filePathResolver = pathResolver;
            WeakReferenceMessenger.Default.Register<RunViewModel, CollectionChangeMessage<BrowserProfile>>(this, OnSourceProfilesCollectionChanged);
            WeakReferenceMessenger.Default.Register<RunViewModel, CollectionChangeMessage<BotInfo>>(this, OnSourceBotsCollectionChanged);
        }

        private void OnSourceProfilesCollectionChanged(RunViewModel recipient, CollectionChangeMessage<BrowserProfile> e)
        {
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    UnselectedProfiles.Add(e.Item);
                    break;
                case CollectionChangeAction.Remove:
                    if (!UnselectedProfiles.Remove(e.Item))
                        SelectedProfiles.Remove(e.Item);
                    break;
            }
        }

        private void OnSourceBotsCollectionChanged(RunViewModel recipient, CollectionChangeMessage<BotInfo> e)
        {
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    Bots.Add(e.Item);
                    break;
                case CollectionChangeAction.Remove:
                    Bots.Remove(e.Item);
                    break;
            }
        }

        #region Methods for command

        private void ExecuteSelectProfile(BrowserProfile? arg)
        {
            ArgumentNullException.ThrowIfNull(arg, nameof(arg));

			UnselectedProfiles.Remove(arg);
			SelectedProfiles.Add(arg);
        }

        private void ExecuteExcludeProfile(BrowserProfile? arg)
        {
            ArgumentNullException.ThrowIfNull(arg, nameof(arg));

            SelectedProfiles.Remove(arg);
			UnselectedProfiles.Add(arg);
        }

        private bool CanExecuteRunBot()
        {
            return SelectedBotStart != null && SelectedProfiles.Count > 0;
        }

        private async void ExecuteRunBot()
        {
            BotJobManager jobManager = _jobManagerFactory.Create(new BotJobContext(SelectedProfiles.ToList(), SelectedBotStart!));
            RunningTasks.Add(jobManager);
            SelectedProfiles.Clear();
            jobManager.Completed += OnJobManager_Completed;
            await jobManager.ExecuteJobsAsync(new BotManagerRunArgs(_filePathResolver.GetPath(FileType.FileBotDll, SelectedBotStart!.Id))).ConfigureAwait(false);
        }

        #endregion

        private void OnJobManager_Completed(object? sender, EventArgs e)
        {
            BotJobManager jobManager = (BotJobManager)sender!;
            jobManager.Completed -= OnJobManager_Completed;

            foreach (BotJob job in jobManager.Jobs)
            {
                UnselectedProfiles.Add(job.BrowserProfile);
            }
        }
    }
}
