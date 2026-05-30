using Microsoft.Playwright;

namespace Playhouse.Core.Models.BrowserEvents.OptionsDecorator
{
    public class PageGoToOptionsStrictDecorator
    {
        private const float DEFAULTTIMEOUT = 30000;
        private const WaitUntilState DEFAULTWAITUNTIL = WaitUntilState.Load;

        private readonly PageGotoOptions _options;

        public string? Referer
        {
            get => _options.Referer;
            set => _options.Referer = value;
        }

        public float Timeout
        {
            get => _options.Timeout ??= DEFAULTTIMEOUT;
            set => _options.Timeout = value;
        }

        public WaitUntilState WaitUntil
        {
            get => _options.WaitUntil ??= DEFAULTWAITUNTIL;
            set => _options.WaitUntil = value;
        }

        // Конструктор для EntityFramework
        private PageGoToOptionsStrictDecorator() 
            : this(null)
        { 
        }

        public PageGoToOptionsStrictDecorator(PageGotoOptions? options = null)
        {
            _options = options ?? new PageGotoOptions();
            _options.Timeout ??= DEFAULTTIMEOUT;
            _options.WaitUntil ??= DEFAULTWAITUNTIL;
        }

        public static explicit operator PageGotoOptions(PageGoToOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator, nameof(decorator));

            return decorator._options; 
        }

        public static explicit operator PageGoToOptionsStrictDecorator(PageGotoOptions options)
        {
            return new PageGoToOptionsStrictDecorator(options);
        }
    }
}