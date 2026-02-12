using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserProfilesViewModel : BaseCollectionViewModel<BrowserProfileViewModel>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly SourceCache<BrowserProfileViewModel, int> _source = new(p => p.Id);

        private ReadOnlyObservableCollection<BrowserProfileViewModel> _profiles;

        public ReadOnlyObservableCollection<BrowserProfileViewModel> Profiles => _profiles;

        public BrowserProfileViewModel? SelectedProfile
        {
            get => field;
            set => SetProperty(field, value, (v) =>
            {
                field = v;
                DeleteProfileCommand.NotifyCanExecuteChanged();
            });
        }

        public IRelayCommand AddProfileCommand => field ??= new RelayCommand(AddProfile);
        public IRelayCommand DeleteProfileCommand => field ??= new AsyncRelayCommand(DeleteProfile, CanDeleteProfile);
        public IRelayCommand SaveProfilesCommand => field ??= new AsyncRelayCommand(SaveProfiles);

        public BrowserProfilesViewModel(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            FillSource(LoadProfilesAsync().Result);
        }

        private async Task<List<BrowserProfileViewModel>> LoadProfilesAsync()
        {
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();

            return await dbContext.BrowserProfiles
                .ToAsyncEnumerable()
                .Select(p => new BrowserProfileViewModel(p))
                .ToListAsync();
        }

        [MemberNotNull(nameof(_profiles))]
        private void FillSource(List<BrowserProfileViewModel> profiles)
        {
            _source.Remove(_source.Items);
            _source.AddOrUpdate(profiles);

            _source.Connect()
                .Bind(out _profiles)
                .Subscribe();
            SendMessageAddItems(profiles);
        }

        private void AddProfile()
        {
            BrowserProfileViewModel newProfile = new();
            _source.AddOrUpdate(newProfile);
            SelectedProfile = newProfile;
        }

        private async Task DeleteProfile()
        {
            if (SelectedProfile == null) 
                return;

            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
            dbContext.BrowserProfiles.Remove(SelectedProfile.Profile);
            await dbContext.SaveChangesAsync();
            SendMessageRemoveItems([SelectedProfile]);
            SelectedProfile = null;
        }

        public bool CanDeleteProfile() => SelectedProfile != null;

        public async Task SaveProfiles()
        {
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
            dbContext.UpdateRange(_profiles);
            await dbContext.SaveChangesAsync();
        }
    }
}