using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;
using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Playhouse.ViewModels.Services.BrowserConfigurationViewModelService;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserConfigurationsViewModel : ViewModelBase, IDisposable
    {
        // Зависимости
        private readonly IBrowserConfigurationViewModelService _browserConfigurationViewModelService;

        private readonly SourceList<BrowserConfigurationViewModel> _savedConfigurationsSource;
        private readonly IDisposable _bindingSavedSource;
        private readonly SourceList<BrowserConfigurationViewModel> _unsavedConfigurationsSource;
        private readonly ReadOnlyObservableCollection<BrowserConfigurationViewModel> _configurations;

        private bool _disposed;

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> Configurations => _configurations;

        public BrowserConfigurationViewModel? SelectedConfiguration
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    DeleteProfileCommand.NotifyCanExecuteChanged();
                    SaveProfileCommand.NotifyCanExecuteChanged();
                    CancelProfileChangesCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IAsyncRelayCommand LoadDataCommand { get; }

        public IAsyncRelayCommand AddProfileCommand { get; }

        public IAsyncRelayCommand<BrowserConfigurationViewModel> SaveProfileCommand { get; }

        public IAsyncRelayCommand<BrowserConfigurationViewModel> DeleteProfileCommand { get; }

        public IRelayCommand<BrowserConfigurationViewModel> CancelProfileChangesCommand { get; }

        public IAsyncRelayCommand SaveAllProfilesCommand { get; }

        public BrowserConfigurationsViewModel(IBrowserConfigurationViewModelService browserConfigurationViewModelService)
        {
            ArgumentNullException.ThrowIfNull(browserConfigurationViewModelService);

            _browserConfigurationViewModelService = browserConfigurationViewModelService;
            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            AddProfileCommand = new AsyncRelayCommand(AddProfile);
            SaveProfileCommand = new AsyncRelayCommand<BrowserConfigurationViewModel>(SaveProfileAsync, CanSaveProfile);
            DeleteProfileCommand = new AsyncRelayCommand<BrowserConfigurationViewModel>(DeleteProfileAsync, CanDeleteProfile);
            CancelProfileChangesCommand = new RelayCommand<BrowserConfigurationViewModel>(CancelProfileChanges, CanCancelProfileChanges);
            SaveAllProfilesCommand = new AsyncRelayCommand(SaveAllProfilesAsync, CanSaveAllProfiles);
            _savedConfigurationsSource = new();
            _unsavedConfigurationsSource = new();
            _bindingSavedSource = _browserConfigurationViewModelService.Configurations
                .ToObservableChangeSet()
                .PopulateInto(_savedConfigurationsSource);
            _savedConfigurationsSource.Connect()
                .OnItemAdded(async (c) => await c.InitializeCommand.ExecuteAsync(null))
                .Or(_unsavedConfigurationsSource.Connect())
                .Bind(out _configurations)
                .AutoRefresh(p => p.IsModified)
                .Subscribe(_ =>
                {
                    SaveAllProfilesCommand.NotifyCanExecuteChanged();
                    SaveProfileCommand.NotifyCanExecuteChanged();
                    CancelProfileChangesCommand.NotifyCanExecuteChanged();
                });
        }

        protected override async Task InitializeCoreAsync()
        {
            foreach (BrowserConfigurationViewModel configuration in _configurations)
            {
                await configuration.InitializeCommand.ExecuteAsync(null);
            }
        }

        private async Task LoadDataAsync()
        {
            await DoBusy(async () =>
            {
                await _browserConfigurationViewModelService.LoadAsync()
                    .ConfigureAwait(false);
            });
        }

        private async Task AddProfile()
        {
            BrowserConfigurationViewModel newProfile = new();
            await newProfile.InitializeCommand.ExecuteAsync(null);
            _unsavedConfigurationsSource.Add(newProfile);
            SelectedConfiguration = newProfile;
        }

        private async Task SaveProfileAsync(BrowserConfigurationViewModel? profile)
        {
            if (!CanSaveProfile(profile))
            {
                return; 
            }

            await DoBusy(async () =>
            {
                await profile.SaveChangesCommand.ExecuteAsync(null)
                    .ConfigureAwait(false);
                await _browserConfigurationViewModelService.SaveAsync(profile)
                    .ConfigureAwait(false);
                _unsavedConfigurationsSource.Remove(profile);
            });
        }

        private bool CanSaveProfile([NotNullWhen(true)] BrowserConfigurationViewModel? profile) 
            => !IsBusy
            && profile != null 
            && profile.SaveChangesCommand.CanExecute(null);

        private async Task DeleteProfileAsync(BrowserConfigurationViewModel? profile)
        {
            if (!CanDeleteProfile(profile))
            {
                return;
            }

            if (profile.Profile.Id == default)
            {
                _unsavedConfigurationsSource.Remove(profile);

                return;
            }

            await DoBusy(async () =>
            {
                await _browserConfigurationViewModelService.DeleteAsync(profile)
                    .ConfigureAwait(false);
            });
        }

        private bool CanDeleteProfile([NotNullWhen(true)] BrowserConfigurationViewModel? profile) 
            => !IsBusy
            && profile != null
            && _configurations.Contains(profile);

        private void CancelProfileChanges(BrowserConfigurationViewModel? profile)
        {
            if (!CanCancelProfileChanges(profile))
            {
                return;
            }

            profile.CancelChangesCommand.Execute(null);
        }

        private bool CanCancelProfileChanges([NotNullWhen(true)] BrowserConfigurationViewModel? profile) 
            => !IsBusy
            && profile != null
            && _configurations.Contains(profile)
            && profile.CancelChangesCommand.CanExecute(null);

        private async Task SaveAllProfilesAsync()
        {
            if (!CanSaveAllProfiles())
            {
                return; 
            }

            List<BrowserConfigurationViewModel> modifiedProfiles = _configurations.Where(p => p.SaveChangesCommand.CanExecute(null)).ToList();

            if (modifiedProfiles.Count > 0)
            {
                foreach (BrowserConfigurationViewModel profile in modifiedProfiles)
                {
                    await profile.SaveChangesCommand.ExecuteAsync(null)
                        .ConfigureAwait(false);
                }

                await _browserConfigurationViewModelService.SaveAsync(modifiedProfiles);
                _unsavedConfigurationsSource.Clear();
            }
        }

        private bool CanSaveAllProfiles() 
            => !IsBusy 
            && _configurations.Any(c => c.SaveChangesCommand.CanExecute(null));

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            GC.SuppressFinalize(this);
            _bindingSavedSource.Dispose();
        }
    }
}