using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserProfileViewModel : ObservableObject
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public bool IsSaving
        {
            get;
            private set
            {
                SetProperty(field, value, (v) =>
                {
                    v = value;
                    SaveCommand.NotifyCanExecuteChanged();
                });
            }
        }

        internal BrowserProfile Profile { get; }

        public int Id => Profile.Id;

        public string Name
        {
            get => Profile.Name;
            set => SetProperty(Profile.Name, value, Profile, (m, v) => m.Name = v);
        }

        public bool AcceptDownloads
        {
            get => Profile.Options.AcceptDownloads;
            set => SetProperty(Profile.Options.AcceptDownloads, value, Profile, (m, v) => m.Options.AcceptDownloads = v);
        }

        public string? Channel
        {
            get => Profile.Options.Channel;
            set => SetProperty(Profile.Options.Channel, value, Profile, (m, v) => m.Options.Channel = v);
        }

        public bool ChromiumSandbox
        {
            get => Profile.Options.ChromiumSandbox;
            set => SetProperty(Profile.Options.ChromiumSandbox, value, Profile, (m, v) => m.Options.ChromiumSandbox = v);
        }

        public string? DownloadsPath
        {
            get => Profile.Options.DownloadsPath;
            set => SetProperty(Profile.Options.DownloadsPath, value, Profile, (m, v) => m.Options.DownloadsPath = v);
        }

        public bool Headless
        {
            get => Profile.Options.Headless;
            set => SetProperty(Profile.Options.Headless, value, Profile, (m, v) => m.Options.Headless = v);
        }

        public float? SlowMo
        {
            get => Profile.Options.SlowMo;
            set => SetProperty(Profile.Options.SlowMo, value, Profile, (m, v) => m.Options.SlowMo = v);
        }

        public IAsyncRelayCommand SaveCommand => field ??= new AsyncRelayCommand(SaveAsync, CanSave);

        public BrowserProfileViewModel(IDbContextFactory<ApplicationDbContext> factory) : this(new BrowserProfile(), factory)
        {
        }

        public BrowserProfileViewModel(BrowserProfile profile, IDbContextFactory<ApplicationDbContext> factory)
        {
            ArgumentNullException.ThrowIfNull(profile, nameof(profile));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            _dbContextFactory = factory;
            Profile = profile;
        }

        private async Task SaveAsync()
        {
            if (IsSaving)
            {
                return; 
            }

            IsSaving = true;

            if (Profile.Id == 0)
            {
                await SaveNewAsync();
            }
            else
            {
                await SaveExistAsync();
            }

            IsSaving = false;
        }

        private bool CanSave() => !IsSaving;

        private async Task SaveNewAsync()
        {
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
            await dbContext.BrowserProfiles.AddAsync(Profile);
            await dbContext.SaveChangesAsync();
            OnPropertyChanged(nameof(Id));
        }

        private async Task SaveExistAsync()
        {
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
            dbContext.BrowserProfiles.Update(Profile);
            await dbContext.SaveChangesAsync();
        }
    }
}
