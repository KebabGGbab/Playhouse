using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.ViewModels.ViewModels.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserProfilesViewModel : BaseCollectionViewModel<BrowserProfileViewModel>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
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
                }
            }
        }

        public bool IsSavingAll
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    SaveAllProfilesCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IRelayCommand AddProfileCommand => field ??= new RelayCommand(AddProfile);

        public IAsyncRelayCommand DeleteProfileCommand => field ??= new AsyncRelayCommand(DeleteProfileAsync, CanDeleteProfile);

        public IAsyncRelayCommand SaveAllProfilesCommand => field ??= new AsyncRelayCommand(SaveAllProfilesAsync, CanSaveAllProfiles);

        public BrowserProfilesViewModel(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
            FillSource(LoadProfilesAsync().Result);
        }

        private async Task<List<BrowserProfileViewModel>> LoadProfilesAsync()
        {
            using ApplicationDbContext dbContext = await _dbFactory.CreateDbContextAsync();

            return await dbContext.BrowserProfiles
                .ToAsyncEnumerable()
                .Select(p => new BrowserProfileViewModel(p, _dbFactory))
                .ToListAsync();
        }

        [MemberNotNull(nameof(_profiles))]
        private void FillSource(List<BrowserProfileViewModel> profiles)
        {
            _source.RemoveMany(_source.Items);
            _source.AddRange(profiles);

            _source.Connect()
                .Bind(out _profiles)
                .AutoRefresh(p => p.IsModifier)
                .Subscribe(_ => SaveAllProfilesCommand.NotifyCanExecuteChanged());
            SendMessageAddItems(profiles);
        }

        private void AddProfile()
        {
            BrowserProfileViewModel newProfile = new(_dbFactory);
            _source.Add(newProfile);
            SelectedProfile = newProfile;
        }

        private async Task DeleteProfileAsync()
        {
            if (SelectedProfile == null)
            {
                return;
            }

            if (!SelectedProfile.IsNew)
            {
                using ApplicationDbContext dbContext = await _dbFactory.CreateDbContextAsync();
                dbContext.BrowserProfiles.Remove(SelectedProfile.Profile);
                await dbContext.SaveChangesAsync();
            }

            _source.Remove(SelectedProfile);
            SendMessageRemoveItems([SelectedProfile]);
        }

        private bool CanDeleteProfile() => SelectedProfile != null;

        private async Task SaveAllProfilesAsync()
        {
            if (!CanSaveAllProfiles())
            {
                return; 
            }

            IsSavingAll = true;
            List<BrowserProfileViewModel> modifiedProfiles = _profiles.Where(p => p.IsModifier).ToList();
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();

            foreach (BrowserProfileViewModel profile in modifiedProfiles)
            {
                await profile.SaveAsync(db);
            }

            await db.SaveChangesAsync();

            foreach(BrowserProfileViewModel profile in modifiedProfiles)
            {
                profile.MarkSaved();
            }

            IsSavingAll = false;
        }

        private bool CanSaveAllProfiles() => _profiles.Any(p => p.IsModifier && !p.IsSaving) && !IsSavingAll;
    }
}