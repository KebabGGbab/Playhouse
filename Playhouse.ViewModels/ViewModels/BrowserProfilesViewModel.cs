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
    public class BrowserProfilesViewModel : BaseCollectionViewModel<BrowserProfileViewModel>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IViewModelFactory<BrowserProfileViewModel, BrowserProfile> _viewModelFactory;
        private readonly SourceList<BrowserProfileViewModel> _source = new();

        private ReadOnlyObservableCollection<BrowserProfileViewModel> _profiles;

        public ReadOnlyObservableCollection<BrowserProfileViewModel> Profiles => _profiles;

        public BrowserProfileViewModel? SelectedProfile
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

        public IAsyncRelayCommand<BrowserProfileViewModel> SaveProfileCommand
            => field ?? new AsyncRelayCommand<BrowserProfileViewModel>(SaveProfileAsync, CanSaveProfile);

        public IAsyncRelayCommand<BrowserProfileViewModel> DeleteProfileCommand 
            => field ??= new AsyncRelayCommand<BrowserProfileViewModel>(DeleteProfileAsync, CanDeleteProfile);

        public IRelayCommand<BrowserProfileViewModel> CancelProfileChangesCommand
            => field ??= new RelayCommand<BrowserProfileViewModel>(CancelProfileChanges, CanCancelProfileChanges);

        public IAsyncRelayCommand SaveAllProfilesCommand => field ??= new AsyncRelayCommand(SaveAllProfilesAsync, CanSaveAllProfiles);

        public BrowserProfilesViewModel(IDbContextFactory<ApplicationDbContext> dbFactory, IViewModelFactory<BrowserProfileViewModel, BrowserProfile> viewModelFactory)
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

            List<BrowserProfileViewModel> profiles = await db.BrowserProfiles
                .Select(p => _viewModelFactory.Create(p))
                .ToListAsync();

            IReadOnlyList<BrowserProfileViewModel> oldItems = _source.Items; 
            _source.RemoveMany(oldItems);
            _source.AddRange(profiles);

            SendMessageRemoveItems((oldItems));
            SendMessageAddItems(profiles);
        }

        private void AddProfile()
        {
            BrowserProfileViewModel newProfile = _viewModelFactory.Create();
            _source.Add(newProfile);
            SelectedProfile = newProfile;
        }

        private async Task SaveProfileAsync(BrowserProfileViewModel? profile)
        {
            if (!CanSaveProfile(profile))
            {
                return; 
            }

            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();
            await profile.SaveAsync(db);
            await db.SaveChangesAsync();
        }

        private bool CanSaveProfile([NotNullWhen(true)] BrowserProfileViewModel? profile) => profile != null;

        private async Task DeleteProfileAsync(BrowserProfileViewModel? profile)
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

        private bool CanDeleteProfile([NotNullWhen(true)] BrowserProfileViewModel? profile) => profile != null;

        private void CancelProfileChanges(BrowserProfileViewModel? profile)
        {
            if (!CanCancelProfileChanges(profile))
            {
                return;
            }

            profile.CancelChanges();
        }

        private bool CanCancelProfileChanges([NotNullWhen(true)] BrowserProfileViewModel? profile) => profile != null;

        private async Task SaveAllProfilesAsync()
        {
            if (!CanSaveAllProfiles() || IsSavingAll)
            {
                return; 
            }

            IsSavingAll = true;
            List<BrowserProfileViewModel> modifiedProfiles = _profiles.Where(p => p.IsModifier).ToList();

            if (modifiedProfiles.Count > 0)
            {

                using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();

                foreach (BrowserProfileViewModel profile in modifiedProfiles)
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