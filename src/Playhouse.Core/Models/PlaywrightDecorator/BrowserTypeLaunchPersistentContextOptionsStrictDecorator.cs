using Microsoft.Playwright;

namespace Playhouse.Core.Models.PlaywrightDecorator
{
    public class BrowserTypeLaunchPersistentContextOptionsStrictDecorator
    {
        private const bool DEFAULT_ACCEPT_DOWNLOADS = true;
        private const bool DEFAULT_CHROMIUM_SANDBOX = false;
        private const bool DEFAULT_HEADLESS = true;

        private static readonly string[] _defaultArgs = ["--disable-blink-features=AutomationControlled"];

        private readonly BrowserTypeLaunchPersistentContextOptions _options;
        private readonly HashSet<string> _args;

        public bool AcceptDownloads
        {
            get => _options.AcceptDownloads ??= DEFAULT_ACCEPT_DOWNLOADS;
            set => _options.AcceptDownloads = value;
        }

        public ISet<string> Args => _args;

        public string? Channel
        {
            get => _options.Channel;
            set => _options.Channel = value;
        }

        public bool ChromiumSandbox
        {
            get => _options.ChromiumSandbox ??= DEFAULT_CHROMIUM_SANDBOX;
            set => _options.ChromiumSandbox = value;
        }

        public string? DownloadsPath
        {
            get => _options.DownloadsPath;
            set => _options.DownloadsPath = value;
        }

        public bool Headless
        {
            get => _options.Headless ??= DEFAULT_HEADLESS;
            set => _options.Headless = value;
        }

        public float? SlowMo
        {
            get => _options.SlowMo;
            set => _options.SlowMo = value;
        }

        // Конструктор для EntityFramework
        public BrowserTypeLaunchPersistentContextOptionsStrictDecorator()
            : this(null)
        {
        }

        public BrowserTypeLaunchPersistentContextOptionsStrictDecorator(BrowserTypeLaunchPersistentContextOptions? options = null)
        {
            _options = options ?? new BrowserTypeLaunchPersistentContextOptions();
            _options.AcceptDownloads ??= DEFAULT_ACCEPT_DOWNLOADS;
            _options.ChromiumSandbox ??= DEFAULT_CHROMIUM_SANDBOX;
            _options.Headless ??= DEFAULT_HEADLESS;
            _args = new(_options.Args ?? _defaultArgs);
            _options.Args = _args;
        }

        public static explicit operator BrowserTypeLaunchPersistentContextOptions(BrowserTypeLaunchPersistentContextOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator);

            return decorator._options;
        }

        public static implicit operator BrowserTypeLaunchPersistentContextOptionsStrictDecorator(BrowserTypeLaunchPersistentContextOptions? options)
        {
            return new BrowserTypeLaunchPersistentContextOptionsStrictDecorator(options);
        }
    }
}
