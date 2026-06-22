using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Playhouse.Domain;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserConfigurationViewModel : EditableViewModel
    {
        private readonly ObservableCollection<VariableViewModel> _userVariables;

        private string _name;
        private bool _acceptDownloads;
        private string? _channel;
        private bool _chromiumSandbox;
        private string? _downloadsPath;
        private bool _headless;
        private bool _javaScriptEnabled;
        private bool _offline;
        private float? _slowMo;
        private string? _userAgent;

        internal BrowserConfiguration Profile { get; }

        public ReadOnlyObservableCollection<VariableViewModel> UserVariables { get; }

        public string NameNewVariable
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    AddVariableCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public string ValueNewVariable
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    AddVariableCommand.NotifyCanExecuteChanged();
                }
            }
        }

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

        public bool JavaScriptEnabled
        {
            get => _javaScriptEnabled;
            set
            {
                if (SetProperty(ref _javaScriptEnabled, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public bool Offline
        {
            get => _offline;
            set
            {
                if (SetProperty(ref _offline, value))
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

        public string? UserAgent
        {
            get => _userAgent;
            set
            {
                if (SetProperty(ref _userAgent, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public IRelayCommand AddVariableCommand { get; }

        public BrowserConfigurationViewModel() 
            : this(new BrowserConfiguration())
        {
        }

        public BrowserConfigurationViewModel(BrowserConfiguration profile)
        {
            ArgumentNullException.ThrowIfNull(profile);

            Profile = profile;
            AddVariableCommand = new RelayCommand(AddVariable, CanAddVariable);
            _name = Profile.Name;
            _acceptDownloads = Profile.Options.AcceptDownloads;
            _channel = Profile.Options.Channel;
            _chromiumSandbox = Profile.Options.ChromiumSandbox;
            _downloadsPath = Profile.Options.DownloadsPath;
            _headless = Profile.Options.Headless;
            _javaScriptEnabled = Profile.Options.JavaScriptEnabled;
            _offline = Profile.Options.Offline;
            _slowMo = Profile.Options.SlowMo;
            _userAgent = Profile.Options.UserAgent;
            _userVariables = new(profile.UserVariables.Select(v => new VariableViewModel(v)));
            UserVariables = new(_userVariables);
            NameNewVariable = string.Empty;
            ValueNewVariable = string.Empty;
        }

        private void AddVariable()
        {
            if (!CanAddVariable())
            {
                return;
            }

            _userVariables.Add(new VariableViewModel(new Variable(NameNewVariable, ValueNewVariable)));
            AddVariableCommand.NotifyCanExecuteChanged();
            IsModified = CheckModified();
        }

        private bool CanAddVariable() =>
            Variable.CanCreate(NameNewVariable, ValueNewVariable)
            && !_userVariables.Any(v => v.Name == NameNewVariable);

        protected override async Task SaveChangesCoreAsync()
        {
            Profile.Name = _name;
            Profile.Options.AcceptDownloads = _acceptDownloads;
            Profile.Options.Channel = _channel;
            Profile.Options.ChromiumSandbox = _chromiumSandbox;
            Profile.Options.DownloadsPath = _downloadsPath;
            Profile.Options.Headless = _headless;
            Profile.Options.JavaScriptEnabled = _javaScriptEnabled;
            Profile.Options.Offline = _offline;
            Profile.Options.SlowMo = _slowMo;
            Profile.Options.UserAgent = _userAgent;
            Profile.UserVariables.Clear();

            foreach (VariableViewModel variableVM in _userVariables)
            {
                if (variableVM.SaveChangesCommand.CanExecute(null))
                {
                    variableVM.SaveChangesCommand.Execute(null);
                }

                Profile.UserVariables.Add(variableVM.Variable);
            }
        }

        protected override void CancelChangesCore()
        {
            Name = Profile.Name;
            AcceptDownloads = Profile.Options.AcceptDownloads;
            Channel = Profile.Options.Channel;
            ChromiumSandbox = Profile.Options.ChromiumSandbox;
            DownloadsPath = Profile.Options.DownloadsPath;
            Headless = Profile.Options.Headless;
            JavaScriptEnabled = Profile.Options.JavaScriptEnabled;
            Offline = Profile.Options.Offline;
            SlowMo = Profile.Options.SlowMo;
            UserAgent = Profile.Options.UserAgent;
            _userVariables.Clear();

            foreach (Variable variable in Profile.UserVariables)
            {
                _userVariables.Add(new VariableViewModel(variable));
            }
        }

        protected override bool CheckModified()
        {
            return !(_name == Profile.Name
                && _acceptDownloads == Profile.Options.AcceptDownloads
                && _channel == Profile.Options.Channel
                && _chromiumSandbox == Profile.Options.ChromiumSandbox
                && _downloadsPath == Profile.Options.DownloadsPath
                && _headless == Profile.Options.Headless
                && _javaScriptEnabled == Profile.Options.JavaScriptEnabled
                && _offline == Profile.Options.Offline
                && _slowMo == Profile.Options.SlowMo
                && _userAgent == Profile.Options.UserAgent
                && Profile.UserVariables.Count == _userVariables.Count
                && !_userVariables.Select(v => v.IsModified).FirstOrDefault(v => v == true)
                && Profile.UserVariables.OrderByDescending(v => v.Name).SequenceEqual(_userVariables.Select(v => v.Variable).OrderByDescending(v => v.Name)));
        }
    }
}