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

        private Memento _memento;

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
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CheckModifier();
                }
            }
        }

        public bool AcceptDownloads
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CheckModifier();
                }
            }
        }

        public string? Channel
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CheckModifier();
                }
            }
        }

        public bool ChromiumSandbox
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CheckModifier();
                }
            }
        }

        public string? DownloadsPath
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CheckModifier();
                }
            }
        }

        public bool Headless
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CheckModifier();
                }
            }
        }

        public float? SlowMo
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    CheckModifier();
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
            Name = profile.Name;
            AcceptDownloads = profile.Options.AcceptDownloads;
            Channel = profile.Options.Channel;
            ChromiumSandbox = profile.Options.ChromiumSandbox;
            DownloadsPath = profile.Options.DownloadsPath;
            Headless = profile.Options.Headless;
            SlowMo = profile.Options.SlowMo;
            _memento = CreateSnapshot();
            IsModifier = IsNew;
        }

        private Memento CreateSnapshot()
        {
            return new Memento(this);
        }

        private void RestoreSnapshot(Memento shapshot)
        {
            Name = shapshot.Name;
            AcceptDownloads = shapshot.AcceptDownloads;
            Channel = shapshot.Channel;
            ChromiumSandbox = shapshot.ChromiumSandbox;
            DownloadsPath = shapshot.DownloadsPath;
            Headless = shapshot.Headless;
            SlowMo = shapshot.SlowMo;
        }

        private void CheckModifier()
        {
            IsModifier = _memento != null 
                && !(_memento.Name == Name 
                && _memento.AcceptDownloads == AcceptDownloads
                && _memento.Channel == Channel
                && _memento.ChromiumSandbox == ChromiumSandbox 
                && _memento.DownloadsPath == DownloadsPath
                && _memento.Headless == Headless
                && _memento.SlowMo == SlowMo);
        }

        private void UpdateModel()
        {
            Profile.Name = Name;
            Profile.Options.AcceptDownloads = AcceptDownloads;
            Profile.Options.Channel = Channel;
            Profile.Options.ChromiumSandbox = ChromiumSandbox;
            Profile.Options.DownloadsPath = DownloadsPath;
            Profile.Options.Headless = Headless;
            Profile.Options.SlowMo = SlowMo;
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
            UpdateModel();

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
            _memento = CreateSnapshot();
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

            RestoreSnapshot(_memento);
            IsModifier = false;
        }

        private bool CanCancelChanges() => IsModifier && !IsSaving;

        private class Memento
        {
            public string Name { get; }
            public bool AcceptDownloads { get; }
            public string? Channel { get; }
            public bool ChromiumSandbox { get; }
            public string? DownloadsPath { get; }
            public bool Headless { get; }
            public float? SlowMo { get; }

            public Memento(BrowserProfileViewModel profile)
            {
                Name = profile.Name;
                AcceptDownloads = profile.AcceptDownloads;
                Channel = profile.Channel;
                ChromiumSandbox = profile.ChromiumSandbox;
                DownloadsPath = profile.DownloadsPath;
                Headless = profile.Headless;
                SlowMo = profile.SlowMo;
            }
        }
    }
}
