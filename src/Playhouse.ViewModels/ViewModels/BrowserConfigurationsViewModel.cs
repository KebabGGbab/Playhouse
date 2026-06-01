using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserConfigurationsViewModel : BaseCollectionViewModel<BrowserConfigurationViewModel>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IViewModelFactory<BrowserConfigurationViewModel, BrowserConfiguration> _viewModelFactory;
        private readonly SourceList<BrowserConfigurationViewModel> _source = new();

        private ReadOnlyObservableCollection<BrowserConfigurationViewModel> _profiles;

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> Profiles => _profiles;

        public BrowserConfigurationViewModel? SelectedProfile
        {
            get => field;
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

        public bool IsSavingAll
        {
            get;
            set => SetProperty(ref field, value);
        }

        public IAsyncRelayCommand LoadDataCommand => field ??= new AsyncRelayCommand(LoadDataAsync);

        public IRelayCommand AddProfileCommand => field ??= new RelayCommand(AddProfile);

        public IAsyncRelayCommand<BrowserConfigurationViewModel> SaveProfileCommand
            => field ?? new AsyncRelayCommand<BrowserConfigurationViewModel>(SaveProfileAsync, CanSaveProfile);

        public IAsyncRelayCommand<BrowserConfigurationViewModel> DeleteProfileCommand 
            => field ??= new AsyncRelayCommand<BrowserConfigurationViewModel>(DeleteProfileAsync, CanDeleteProfile);

        public IRelayCommand<BrowserConfigurationViewModel> CancelProfileChangesCommand
            => field ??= new RelayCommand<BrowserConfigurationViewModel>(CancelProfileChanges, CanCancelProfileChanges);

        public IAsyncRelayCommand SaveAllProfilesCommand => field ??= new AsyncRelayCommand(SaveAllProfilesAsync, CanSaveAllProfiles);

        public BrowserConfigurationsViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, IViewModelFactory<BrowserConfigurationViewModel, BrowserConfiguration> viewModelFactory)
        {
            ArgumentNullException.ThrowIfNull(dbFactory, nameof(dbFactory));
            ArgumentNullException.ThrowIfNull(viewModelFactory, nameof(viewModelFactory));

            _dbFactory = dbFactory;
            _viewModelFactory = viewModelFactory;

            _source.Connect()
                .Bind(out _profiles)
                .AutoRefresh(p => p.IsModifier)
                .Subscribe(_ => SaveAllProfilesCommand.NotifyCanExecuteChanged());
        }

        private async Task LoadDataAsync()
        {
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();

            List<BrowserConfigurationViewModel> profiles = await db.Profiles
                .Select(p => _viewModelFactory.Create(p))
                .ToListAsync();

            IReadOnlyList<BrowserConfigurationViewModel> oldItems = _source.Items; 
            _source.RemoveMany(oldItems);
            _source.AddRange(profiles);

            SendMessageRemoveItems((oldItems));
            SendMessageAddItems(profiles);
        }

        private void AddProfile()
        {
            BrowserConfigurationViewModel newProfile = _viewModelFactory.Create();
            _source.Add(newProfile);
            SelectedProfile = newProfile;
        }

        private async Task SaveProfileAsync(BrowserConfigurationViewModel? profile)
        {
            if (!CanSaveProfile(profile))
            {
                return; 
            }

            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();
            await profile.SaveAsync(db);
            await db.SaveChangesAsync();
        }

        private bool CanSaveProfile([NotNullWhen(true)] BrowserConfigurationViewModel? profile) => profile != null;

        private async Task DeleteProfileAsync(BrowserConfigurationViewModel? profile)
        {
            if (!CanDeleteProfile(profile))
            {
                return;
            }

            if (!profile.IsNew)
            {
                using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();
                await profile.DeleteAsync(db);
                await db.SaveChangesAsync();
            }

            _source.Remove(profile);
            SendMessageRemoveItems([profile]);
        }

        private bool CanDeleteProfile([NotNullWhen(true)] BrowserConfigurationViewModel? profile) => profile != null;

        private void CancelProfileChanges(BrowserConfigurationViewModel? profile)
        {
            if (!CanCancelProfileChanges(profile))
            {
                return;
            }

            profile.CancelChanges();
        }

        private bool CanCancelProfileChanges([NotNullWhen(true)] BrowserConfigurationViewModel? profile) => profile != null;

        private async Task SaveAllProfilesAsync()
        {
            if (!CanSaveAllProfiles() || IsSavingAll)
            {
                return; 
            }

            IsSavingAll = true;
            List<BrowserConfigurationViewModel> modifiedProfiles = _profiles.Where(p => p.IsModifier).ToList();

            if (modifiedProfiles.Count > 0)
            {

                using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();

                foreach (BrowserConfigurationViewModel profile in modifiedProfiles)
                {
                    await profile.SaveAsync(db);
                }

                await db.SaveChangesAsync();
            }

            IsSavingAll = false;
        }

        private bool CanSaveAllProfiles() => _profiles.Count > 0;
    }
}