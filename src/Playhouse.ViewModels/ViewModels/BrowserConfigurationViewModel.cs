using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Playhouse.Domain;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserConfigurationViewModel : EditableViewModel
    {
        private string _name;
        private bool _acceptDownloads;
        private string? _channel;
        private bool _chromiumSandbox;
        private string? _downloadsPath;
        private bool _headless;
        private float? _slowMo;

        internal BrowserConfiguration Profile { get; }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    IsModified = CheckModified();
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
                    IsModified = CheckModified();
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
                    IsModified = CheckModified();
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
                    IsModified = CheckModified();
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
                    IsModified = CheckModified();
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
                    IsModified = CheckModified();
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
                    IsModified = CheckModified();
                }
            }
        }

        public BrowserConfigurationViewModel() 
            : this(new BrowserConfiguration())
        {
        }

        public BrowserConfigurationViewModel(BrowserConfiguration profile)
        {
            ArgumentNullException.ThrowIfNull(profile);

            Profile = profile;
            _name = Profile.Name;
            _acceptDownloads = Profile.Options.AcceptDownloads;
            _channel = Profile.Options.Channel;
            _chromiumSandbox = Profile.Options.ChromiumSandbox;
            _downloadsPath = Profile.Options.DownloadsPath;
            _headless = Profile.Options.Headless;
            _slowMo = Profile.Options.SlowMo;
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Profile.Name = Name;
            Profile.Options.AcceptDownloads = AcceptDownloads;
            Profile.Options.Channel = Channel;
            Profile.Options.ChromiumSandbox = ChromiumSandbox;
            Profile.Options.DownloadsPath = DownloadsPath;
            Profile.Options.Headless = Headless;
            Profile.Options.SlowMo = SlowMo;
        }

        protected override void CancelChangesCore()
        {
            Name = Profile.Name;
            AcceptDownloads = Profile.Options.AcceptDownloads;
            Channel = Profile.Options.Channel;
            ChromiumSandbox = Profile.Options.ChromiumSandbox;
            DownloadsPath = Profile.Options.DownloadsPath;
            Headless = Profile.Options.Headless;
            SlowMo = Profile.Options.SlowMo;
        }

        protected override bool CheckModified()
        {
            return !(Profile.Name == Name
                && Profile.Options.AcceptDownloads == AcceptDownloads
                && Profile.Options.Channel == Channel
                && Profile.Options.ChromiumSandbox == ChromiumSandbox
                && Profile.Options.DownloadsPath == DownloadsPath
                && Profile.Options.Headless == Headless
                && Profile.Options.SlowMo == SlowMo);
        }
    }
}