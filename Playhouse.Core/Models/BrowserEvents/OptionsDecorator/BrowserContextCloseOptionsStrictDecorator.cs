using Microsoft.Playwright;

namespace Playhouse.Core.Models.BrowserEvents.OptionsDecorator
{
    public class BrowserContextCloseOptionsStrictDecorator
    {
        private readonly BrowserContextCloseOptions _options;

        public string? Reason
        {
            get => _options?.Reason;
            set => _options?.Reason = value;
        }

        // Конструктор для EntityFramework
        private BrowserContextCloseOptionsStrictDecorator()
            : this(null)
        {
        }

        public BrowserContextCloseOptionsStrictDecorator(BrowserContextCloseOptions? options = null)
        {
            _options = options ?? new BrowserContextCloseOptions();
        }

        public static explicit operator BrowserContextCloseOptions(BrowserContextCloseOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator, nameof(decorator));

            return decorator._options;
        }

        public static explicit operator BrowserContextCloseOptionsStrictDecorator(BrowserContextCloseOptions options)
        {
            return new BrowserContextCloseOptionsStrictDecorator(options);
        }
    }
}
