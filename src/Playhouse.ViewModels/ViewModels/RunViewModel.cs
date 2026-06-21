using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using DynamicData.Binding;
using Playhouse.Application.Services.BotRunningService;
using Playhouse.ViewModels.Messages;
using Playhouse.ViewModels.Services.BrowserConfigurationViewModelService;

namespace Playhouse.ViewModels.ViewModels
{
    public class RunViewModel : ObservableObject, IDisposable
    {
        // Зависимости
        private readonly IRunServiceFactory _runServiceFactory;
        private readonly IBrowserConfigurationViewModelService _browserConfigurationViewModelService;

        private readonly SourceList<BrowserConfigurationViewModel> _browserConfigurationsSource;
        private readonly IDisposable _bindingBrowserConfigurationsSource;
        private readonly ObservableCollection<BrowserConfigurationViewModel> _unselectedProfiles;
        private readonly ObservableCollection<BrowserConfigurationViewModel> _selectedProfiles;
        private readonly ObservableCollection<BotConfigurationViewModel> _bots = [];
        private readonly ObservableCollection<RunServiceViewModel> _runningTasks = [];
        private readonly Dictionary<RunServiceViewModel, BrowserConfigurationViewModel[]> _busyProfiles = [];

        private bool _disposed;

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

        public RunViewModel(IRunServiceFactory runServiceFactory, IBrowserConfigurationViewModelService browserConfigurationViewModelService)
        {
            ArgumentNullException.ThrowIfNull(browserConfigurationViewModelService);

            _runServiceFactory = runServiceFactory;
            _browserConfigurationViewModelService = browserConfigurationViewModelService;
            Bots = new(_bots);
            RunningTasks = new(_runningTasks);
            WeakReferenceMessenger.Default.Register<RunViewModel, CollectionChangedMessage<BotConfigurationViewModel>>(this, OnSourceBotsCollectionChanged);
            SelectProfileCommand = new RelayCommand<BrowserConfigurationViewModel>(SelectProfile);
            ExcludeProfileCommand = new RelayCommand<BrowserConfigurationViewModel>(ExcludeProfile);
            RunBotCommand = new RelayCommand(ExecuteRunBot, CanExecuteRunBot);
            _unselectedProfiles = [];
            UnselectedProfiles = new ReadOnlyObservableCollection<BrowserConfigurationViewModel>(_unselectedProfiles);
            _selectedProfiles = [];
            SelectedProfiles = new ReadOnlyObservableCollection<BrowserConfigurationViewModel>(_selectedProfiles);
            _browserConfigurationsSource = new();
            _bindingBrowserConfigurationsSource = _browserConfigurationViewModelService.Configurations
                .ToObservableChangeSet()
                .PopulateInto(_browserConfigurationsSource);
            _browserConfigurationsSource.Connect()
                .OnItemAdded(BrowserConfigurationAddedToSource)
                .OnItemRemoved(BrowserConfigurationDeletedFromSource)
                .Subscribe();
        }

        private void BrowserConfigurationAddedToSource(BrowserConfigurationViewModel configuration)
        {
            _unselectedProfiles.Add(configuration);
        }

        private void BrowserConfigurationDeletedFromSource(BrowserConfigurationViewModel configuration)
        {
            if (!_unselectedProfiles.Remove(configuration))
                if (!_selectedProfiles.Remove(configuration))
                {
                    // TODO: сделать удаление из занятых браузеров
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

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            GC.SuppressFinalize(this);
            _bindingBrowserConfigurationsSource.Dispose();
            _browserConfigurationsSource.Dispose();
        }
    }
}
