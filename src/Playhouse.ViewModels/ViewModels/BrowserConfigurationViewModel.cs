using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Domain;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserConfigurationViewModel : ObservableObject
    {
        private readonly FileManager<BrowserConfiguration> _fileManager;

        private string _name;
        private bool _acceptDownloads;
        private string? _channel;
        private bool _chromiumSandbox;
        private string? _downloadsPath;
        private bool _headless;
        private float? _slowMo;

        internal BrowserConfiguration Profile { get; }

        public bool IsSaving
        {
            get;
            private set => SetProperty(ref field, value);
        }

        public bool IsNew 
        {
            get => field;
            set => SetProperty(ref field, value);
        }

        public bool IsModifier
        {
            get;
            private set => SetProperty(ref field, value);
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

        public BrowserConfigurationViewModel(FileManager<BrowserConfiguration> fileManager) 
            : this(new BrowserConfiguration(), fileManager)
        {
        }

        public BrowserConfigurationViewModel(BrowserConfiguration profile, FileManager<BrowserConfiguration> fileManager)
        {
            ArgumentNullException.ThrowIfNull(fileManager, nameof(fileManager));
            ArgumentNullException.ThrowIfNull(profile, nameof(profile));

            _fileManager = fileManager;
            Profile = profile;
            _name = Profile.Name;
            _acceptDownloads = Profile.Options.AcceptDownloads;
            _channel = Profile.Options.Channel;
            _chromiumSandbox = Profile.Options.ChromiumSandbox;
            _downloadsPath = Profile.Options.DownloadsPath;
            _headless = Profile.Options.Headless;
            _slowMo = Profile.Options.SlowMo;
            IsNew = Id == 0;
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

        public async Task SaveAsync(DbContext db)
        {
            if (!CanSave())
            {
                return; 
            }

            IsSaving = true;
            ApplyChanges();
            db.SavedChanges += OnProfileSaved;

            if (IsNew)
            {
                await db.Set<BrowserConfiguration>().AddAsync(Profile);
            }
            else
            {
                db.Set<BrowserConfiguration>().Update(Profile);
            }
        }

        private void OnProfileSaved(object? sender, SavedChangesEventArgs e)
        {
            if (sender is not DbContext db)
            {
                return;
            }

            db.SavedChanges -= OnProfileSaved;

            if (IsNew)
            {
                _fileManager.Create(Profile);
                IsNew = false;
                NotifyChangedOneTimeSetProperty();
            }

            IsModifier = false;
            IsSaving = false;
        }

        private bool CanSave() => IsModifier && !IsSaving;

        public async Task DeleteAsync(DbContext db)
        {
            if (!IsNew)
            {
                db.SavedChanges += OnProfileDeleted;
                db.Set<BrowserConfiguration>().Remove(Profile);
            }
        }

        private void OnProfileDeleted(object? sender, SavedChangesEventArgs e)
        {
            if (sender is not DbContext db)
            {
                return;
            }

            db.SavedChanges -= OnProfileDeleted;

            _fileManager.Delete(Profile);
        }

        public void CancelChanges()
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