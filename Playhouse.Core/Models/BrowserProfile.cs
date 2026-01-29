using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Playwright;
using Playhouse.Core.Models.Adapters;

namespace Playhouse.Core.Models
{
	public sealed partial class BrowserProfile : ObservableObject
    {
        public string Name
        {
            get;
            set => SetProperty(ref field, value);
        } = string.Empty;

        public int Id
        {
            get;
            set => SetProperty(ref field, value);
        }

        public BrowserTypeLaunchPersistentContextOptions Options
        {
            get;
            init => SetProperty(ref field, value);
        } = new();

        #region Storage

        [JsonIgnore]
        public bool? AcceptDownloads
        {
            get => Options.AcceptDownloads;
            set => SetProperty(Options.AcceptDownloads, value, Options, (m, v) => m.AcceptDownloads = v);
        }

        [JsonIgnore]
        public string? DownloadsPath
        {
            get => Options.DownloadsPath;
            set => SetProperty(Options.DownloadsPath, value, Options, (m, v) => m.DownloadsPath = v);
        }

        [JsonIgnore]
        public HarContentPolicy? RecordHarContent
        {
            get => Options.RecordHarContent;
            set => SetProperty(Options.RecordHarContent, value, Options, (m, v) => m.RecordHarContent = v);
        }

        [JsonIgnore]
        public HarMode? RecordHarMode
        {
            get => Options.RecordHarMode;
            set => SetProperty(Options.RecordHarMode, value, Options, (m, v) => m.RecordHarMode = v);
        }

        [JsonIgnore]
        public bool? RecordHarOmitContent
        {
            get => Options.RecordHarOmitContent;
            set => SetProperty(Options.RecordHarOmitContent, value, Options, (m, v) => m.RecordHarOmitContent = v);
        }

        [JsonIgnore]
        public string? RecordHarPath
        {
            get => Options.RecordHarPath;
            set => SetProperty(Options.RecordHarPath, value, Options, (m, v) => m.RecordHarPath = v);
        }

        [JsonIgnore]
        public string? RecordVideoDir
        {
            get => Options.RecordVideoDir;
            set => SetProperty(Options.RecordVideoDir, value, Options, (m, v) => m.RecordVideoDir = v);
        }

        [JsonIgnore]
        public RecordVideoSizeAdapters? RecordVideoSize
        {
            get => field ??= new(Options.RecordVideoSize ??= new RecordVideoSize());
            set => SetProperty(ref field, value);
        }

        [JsonIgnore]
        public string? TracesDir
        {
            get => Options.TracesDir;
            set => SetProperty(Options.TracesDir, value, Options, (m, v) => m.TracesDir = v);
        }

        #endregion

        #region Network

        [JsonIgnore]
        public string? BaseURL
        {
            get => Options.BaseURL;
            set => SetProperty(Options.BaseURL, value, Options, (m, v) => m.BaseURL = v);
        }

        // TODO: доделать коллекции
        [JsonIgnore]
        public ObservableCollection<ExtraHTTPHeader> ExtraHttpHeaders
        {
            get => field;
            init
            {
                field = value;
                Options.ExtraHTTPHeaders = field.ToDictionary(e => e.Header, e => e.Value);

                field.CollectionChanged += (s, e) =>
                {
                    Options.ExtraHTTPHeaders = field.Where(e => e.Header != null).ToDictionary(e => e.Header, e => e.Value);
                };
            }
        }

        [JsonIgnore]
        public HttpCredentialsAdapter HttpCredentialsWrapper
        {
            get => field ??= new(Options.HttpCredentials ??= new());
            set => SetProperty(ref field, value);
        }

        [JsonIgnore]
        public bool? IgnoreHTTPSErrors
        {
            get => Options.IgnoreHTTPSErrors;
            set => SetProperty(Options.IgnoreHTTPSErrors, value, Options, (m, v) => m.IgnoreHTTPSErrors = v);
        }

        //TODO: это не надо
        //[JsonIgnore]
        //public ObservableCollection<string>? Permissions
        //{
        //    get
        //    {
        //        return _permissions;
        //    }
        //    set
        //    {
        //        if (_permissions == value)
        //        {
        //            return;
        //        }

        //        _permissions = value;
        //        OnPropertyChanged();
        //    }
        //}

        [JsonIgnore]
        public ProxyAdapter ProxyWrapper
        {
            get => field ??= new(Options.Proxy ??= new());
            set => SetProperty(ref field, value);
        }

        [JsonIgnore]
        public bool? Offline
        {
            get => Options.Offline;
            set => SetProperty(Options.Offline, value, Options, (m, v) => m.Offline = v);
        }

        #endregion

        #region Environment emulation

        [JsonIgnore]
        public GeolocationAdapter? GeolocationWrapper
        {
            get => field ??= new(Options.Geolocation ??= new());
            set => SetProperty(ref field, value);
        }

        [JsonIgnore]
        public string? TimezoneId
        {
            get => Options.TimezoneId;
            set => SetProperty(Options.TimezoneId, value, Options, (m, v) => m.TimezoneId = v);
        }

        [JsonIgnore]
        public string? Locale
        {
            get => Options.Locale;
            set => SetProperty(Options.Locale, value, Options, (m, v) => m.Locale = v);
        }

        [JsonIgnore]
        public ColorScheme? MediaColorScheme
        {
            get => Options.ColorScheme;
            set => SetProperty(Options.ColorScheme, value, Options, (m, v) => m.ColorScheme = v);
        }

        [JsonIgnore]
        public Contrast? MediaContrast
        {
            get => Options.Contrast;
            set => SetProperty(Options.Contrast, value, Options, (m, v) => m.Contrast = v);
        }

        [JsonIgnore]
        public ForcedColors? MediaForcedColors
        {
            get => Options.ForcedColors;
            set => SetProperty(Options.ForcedColors, value, Options, (m, v) => m.ForcedColors = v);
        }

        [JsonIgnore]
        public ReducedMotion? MediaReducedMotion
        {
            get => Options.ReducedMotion;
            set => SetProperty(Options.ReducedMotion, value, Options, (m, v) => m.ReducedMotion = v);
        }

        [JsonIgnore]
        public float? DeviceScaleFactor
        {
            get => Options.DeviceScaleFactor;
            set => SetProperty(Options.DeviceScaleFactor, value, Options, (m, v) => m.DeviceScaleFactor = v);
        }

        [JsonIgnore]
        public bool? HasTouch
        {
            get => Options.HasTouch;
            set => SetProperty(Options.HasTouch, value, Options, (m, v) => m.HasTouch = v);
        }

        [JsonIgnore]
        public bool? IsMobile
        {
            get => Options.IsMobile;
            set => SetProperty(Options.IsMobile, value, Options, (m, v) => m.IsMobile = v);
        }

        // TODO: вероятно нужно будет заменить на декоратор
        [JsonIgnore]
        public ScreenSize? ScreenSize
        {
            get => Options.ScreenSize ??= new();
            set => SetProperty(Options.ScreenSize, value, Options, (m, v) => m.ScreenSize = v);
        }

        [JsonIgnore]
        public ServiceWorkerPolicy? ServiceWorkers
        {
            get => Options.ServiceWorkers;
            set => SetProperty(Options.ServiceWorkers, value, Options, (m, v) => m.ServiceWorkers = v);
        }

        [JsonIgnore]
        public string? UserAgent
        {
            get => Options.UserAgent;
            set => SetProperty(Options.UserAgent, value, Options, (m, v) => m.UserAgent = v);
        }

        // TODO: вероятно нужно будет заменить на декоратор
        [JsonIgnore]
        public ViewportSize? ViewportSize
        {
            get => Options.ViewportSize ??= new();
            set => SetProperty(Options.ViewportSize, value, Options, (m, v) => m.ViewportSize = v);
        }

        #endregion

        #region Launching and configuring

        [JsonIgnore]
        public ObservableCollection<BrowserArgument> Args
        {
            get
            {
                IEnumerable<string> args = Options.Args ??= [];
                return new(args.Select(s => new BrowserArgument(s)));
            }
            set
            {
                IEnumerable<string> args = value.Select(a => a.Argument);
                Options.Args = args;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string? Channel
        {
            get => Options.Channel;
            set => SetProperty(Options.Channel, value, Options, (m, v) => m.Channel = v);
        }

        [JsonIgnore]
        public bool? ChromiumSandbox
        {
            get => Options.ChromiumSandbox;
            set => SetProperty(Options.ChromiumSandbox, value, Options, (m, v) => m.ChromiumSandbox = v);
        }

        //TODO: это не надо
        //[JsonIgnore]
        //public IEnumerable<ClientCertificate>? ClientCertificates
        //{
        //    get
        //    {
        //        return _clientCertificates;
        //    }
        //    set
        //    {
        //        if (_clientCertificates == value)
        //        {
        //            return;
        //        }

        //        _clientCertificates = value;
        //        OnPropertyChanged();
        //    }
        //}

        [JsonIgnore]
        public string? ExecutablePath
        {
            get => Options.ExecutablePath;
            set => SetProperty(Options.ExecutablePath, value, Options, (m, v) => m.ExecutablePath = v);
        }

        [JsonIgnore]
        public bool? Headless
        {
            get => Options.Headless;
            set => SetProperty(Options.Headless, value, Options, (m, v) => m.Headless = v);
        }

        [JsonIgnore]
        public float? SlowMo
        {
            get => Options.SlowMo;
            set => SetProperty(Options.SlowMo, value, Options, (m, v) => m.SlowMo = v);
        }

        [JsonIgnore]
        public float? Timeout
        {
            get => Options.Timeout;
            set => SetProperty(Options.Timeout, value, Options, (m, v) => m.Timeout = v);
        }

        #endregion

        #region Browser behavior

        [JsonIgnore]
        public bool? BypassCSP
        {
            get => Options.BypassCSP;
            set => SetProperty(Options.BypassCSP, value, Options, (m, v) => m.BypassCSP = v);
        }

        //TODO: это не надо
        //[JsonIgnore]
        //public Dictionary<string, string>? Env
        //{
        //    get
        //    {
        //        return _env;
        //    }
        //    set
        //    {
        //        if (_env == value)
        //        {
        //            return;
        //        }

        //        _env = value;
        //        OnPropertyChanged();
        //    }
        //}

        [JsonIgnore]
        public bool? HandleSIGHUP
        {
            get => Options.HandleSIGHUP;
            set => SetProperty(Options.HandleSIGHUP, value, Options, (m, v) => m.HandleSIGHUP = v);
        }

        [JsonIgnore]
        public bool? HandleSIGINT
        {
            get => Options.HandleSIGINT;
            set => SetProperty(Options.HandleSIGINT, value, Options, (m, v) => m.HandleSIGINT = v);
        }

        [JsonIgnore]
        public bool? HandleSIGTERM
        {
            get => Options.HandleSIGTERM;
            set => SetProperty(Options.HandleSIGTERM, value, Options, (m, v) => m.HandleSIGTERM = v);
        }

        [JsonIgnore]
        public bool? IgnoreAllDefaultArgs
        {
            get => Options.IgnoreAllDefaultArgs;
            set => SetProperty(Options.IgnoreAllDefaultArgs, value, Options, (m, v) => m.IgnoreAllDefaultArgs = v);
        }

        //TODO: это не надо
        //[JsonIgnore]
        //public IEnumerable<string>? IgnoreDefaultArgs
        //{
        //    get
        //    {
        //        return _ignoreDefaultArgs;
        //    }
        //    set
        //    {
        //        if (_ignoreDefaultArgs == value)
        //        {
        //            return;
        //        }

        //        _ignoreDefaultArgs = value;
        //        OnPropertyChanged();
        //    }
        //}

        [JsonIgnore]
        public bool? JavaScriptEnabled
        {
            get => Options.JavaScriptEnabled;
            set => SetProperty(Options.JavaScriptEnabled, value, Options, (m, v) => m.JavaScriptEnabled = v);
        }

        [JsonIgnore]
        public bool? StrictSelectors
        {
            get => Options.StrictSelectors;
            set => SetProperty(Options.StrictSelectors, value, Options, (m, v) => m.StrictSelectors = v);
        }

        #endregion

        #region Specific browser settings

        //TODO: это не надо
        //[JsonIgnore]
        //public Dictionary<string, object>? FirefoxUserPrefs
        //{
        //    get
        //    {
        //        return _firefoxUserPrefs;
        //    }
        //    set
        //    {
        //        if (_firefoxUserPrefs == value)
        //        {
        //            return;
        //        }

        //        _firefoxUserPrefs = value;
        //        OnPropertyChanged();
        //    }
        //}

        #endregion

        public BrowserProfile()
        {
            AcceptDownloads = true;
            RecordHarContent = HarContentPolicy.Embed;
            RecordHarMode = HarMode.Full;
            RecordHarOmitContent = false;
            IgnoreHTTPSErrors = false;
            MediaColorScheme = ColorScheme.Light;
            MediaContrast = Contrast.NoPreference;
            MediaForcedColors = ForcedColors.None;
            MediaReducedMotion = ReducedMotion.NoPreference;
            DeviceScaleFactor = 1F;
            HasTouch = false;
            IsMobile = false;
            ServiceWorkers = ServiceWorkerPolicy.Allow;
            ChromiumSandbox = false;
            Headless = false;
            Timeout = 30000F;
            BypassCSP = false;
            HandleSIGHUP = true;
            HandleSIGINT = true;
            HandleSIGTERM = true;
            IgnoreAllDefaultArgs = false;
            JavaScriptEnabled = true;
            StrictSelectors = false;
            ExtraHttpHeaders = [];
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append('[');
            sb.Append(Id);
            sb.Append(']');
            sb.Append(new string(' ', 5));
            sb.Append(Name);

            return sb.ToString();
        }
    }
}