using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserProfileViewModel : ObservableObject
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        private string _name;
        private bool _acceptDownloads;
        private string? _channel;
        private bool _chromiumSandbox;
        private string? _downloadsPath;
        private bool _headless;
        private float? _slowMo;

        internal BrowserProfile Profile { get; }

        public bool IsSaving
        {
            get;
            private set
            {
                if (SetProperty(ref field, value))
                {
                    SaveCommand.NotifyCanExecuteChanged();
                    CancelChangesCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public bool IsNew => Id == 0;

        public bool IsModifier
        {
            get;
            private set
            {
                if (SetProperty(ref field, value))
                {
                    SaveCommand.NotifyCanExecuteChanged();
                    CancelChangesCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int Id => Profile.Id;

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    CheckChanges();
                }
            }
        }

        public bool AcceptDownloads
        {
            get => _acceptDownloads;
            set
            {
                if (SetProperty(ref _acceptDownloads, value))
                {
                    CheckChanges();
                }
            }
        }

        public string? Channel
        {
            get => _channel;
            set
            {
                if (SetProperty(ref _channel, value))
                {
                    CheckChanges();
                }
            }
        }

        public bool ChromiumSandbox
        {
            get => _chromiumSandbox;
            set
            {
                if (SetProperty(ref _chromiumSandbox, value))
                {
                    CheckChanges();
                }
            }
        }

        public string? DownloadsPath
        {
            get => _downloadsPath;
            set
            {
                if (SetProperty(ref _downloadsPath, value))
                {
                    CheckChanges();
                }
            }
        }

        public bool Headless
        {
            get => _headless;
            set
            {
                if (SetProperty(ref _headless, value))
                {
                    CheckChanges();
                }
            }
        }

        public float? SlowMo
        {
            get => _slowMo;
            set
            {
                if (SetProperty(ref _slowMo, value))
                {
                    CheckChanges();
                }
            }
        }

        public IAsyncRelayCommand SaveCommand => field ??= new AsyncRelayCommand(SaveAsync, CanSave);

        public IRelayCommand CancelChangesCommand => field ??= new RelayCommand(CancelChanges, CanCancelChanges);

        public BrowserProfileViewModel(IDbContextFactory<ApplicationDbContext> factory) : this(new BrowserProfile(), factory)
        {
        }

        public BrowserProfileViewModel(BrowserProfile profile, IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            ArgumentNullException.ThrowIfNull(profile, nameof(profile));
            ArgumentNullException.ThrowIfNull(dbFactory, nameof(dbFactory));

            _dbFactory = dbFactory;
            Profile = profile;
            ResetChanges();
            IsModifier = IsNew;
        }

        private void CheckChanges()
        {
            IsModifier = !(Profile.Name == Name 
                && Profile.Options.AcceptDownloads == AcceptDownloads
                && Profile.Options.Channel == Channel
                && Profile.Options.ChromiumSandbox == ChromiumSandbox 
                && Profile.Options.DownloadsPath == DownloadsPath
                && Profile.Options.Headless == Headless
                && Profile.Options.SlowMo == SlowMo);
        }

        private void ApplyChanges()
        {
            Profile.Name = Name;
            Profile.Options.AcceptDownloads = AcceptDownloads;
            Profile.Options.Channel = Channel;
            Profile.Options.ChromiumSandbox = ChromiumSandbox;
            Profile.Options.DownloadsPath = DownloadsPath;
            Profile.Options.Headless = Headless;
            Profile.Options.SlowMo = SlowMo;
        }

        [MemberNotNull(nameof(_name))]
        private void ResetChanges()
        {
            Name = Profile.Name;
            AcceptDownloads = Profile.Options.AcceptDownloads;
            Channel = Profile.Options.Channel;
            ChromiumSandbox = Profile.Options.ChromiumSandbox;
            DownloadsPath = Profile.Options.DownloadsPath;
            Headless = Profile.Options.Headless;
            SlowMo = Profile.Options.SlowMo;
        }

        private void NotifyChangedOneTimeSetProperty()
        {
            OnPropertyChanged(nameof(Id));
        }

        private async Task SaveAsync()
        {
            if (!CanSave())
            {
                return; 
            }

            IsSaving = true;
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync();
            await SaveAsync(db);
            await db.SaveChangesAsync();
            MarkSaved();
        }

        internal async Task SaveAsync(ApplicationDbContext db)
        {
            IsSaving = true;
            ApplyChanges();

            if (IsNew)
            {
                await db.BrowserProfiles.AddAsync(Profile);
            }
            else
            {
                db.BrowserProfiles.Update(Profile);
            }
        }

        internal void MarkSaved()
        {
            NotifyChangedOneTimeSetProperty();
            IsModifier = false;
            IsSaving = false;
        }

        private bool CanSave() => IsModifier && !IsSaving;

        private void CancelChanges()
        {
            if (!CanCancelChanges())
            {
                return;
            }

            ResetChanges();
            IsModifier = false;
        }

        private bool CanCancelChanges() => IsModifier && !IsSaving;
    }
}