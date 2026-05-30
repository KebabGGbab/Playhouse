using Microsoft.Playwright;

namespace Playhouse.Core.Models.PlaywrightDecorator
{
    public class BrowserTypeLaunchPersistentContextOptionsStrictDecorator
    {
        private const bool DEFAULTACCEPTDOWNLOADS = true;
        private const bool DEFAULTCHROMIUMSANDBOX = false;
        private const bool DEFAULTHEADLESS = true;

        private readonly BrowserTypeLaunchPersistentContextOptions _options;

        public bool AcceptDownloads
        {
            get => _options.AcceptDownloads ??= DEFAULTACCEPTDOWNLOADS;
            set => _options.AcceptDownloads = value;
        }

        public string? Channel
        {
            get => _options.Channel;
            set => _options.Channel = value;
        }

        public bool ChromiumSandbox
        {
            get => _options.ChromiumSandbox ??= DEFAULTCHROMIUMSANDBOX;
            set => _options.ChromiumSandbox = value;
        }

        public string? DownloadsPath
        {
            get => _options.DownloadsPath;
            set => _options.DownloadsPath = value;
        }

        public bool Headless
        {
            get => _options.Headless ??= DEFAULTHEADLESS;
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
            _options.AcceptDownloads ??= DEFAULTACCEPTDOWNLOADS;
            _options.ChromiumSandbox ??= DEFAULTCHROMIUMSANDBOX;
            _options.Headless ??= DEFAULTHEADLESS;
        }

        public static explicit operator BrowserTypeLaunchPersistentContextOptions(BrowserTypeLaunchPersistentContextOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator, nameof(decorator));

            return decorator._options;
        }

        public static implicit operator BrowserTypeLaunchPersistentContextOptionsStrictDecorator(BrowserTypeLaunchPersistentContextOptions options)
        {
            return new BrowserTypeLaunchPersistentContextOptionsStrictDecorator(options);
        }
    }
}
