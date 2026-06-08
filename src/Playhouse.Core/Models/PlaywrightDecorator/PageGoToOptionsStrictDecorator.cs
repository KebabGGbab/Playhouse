using Microsoft.Playwright;

namespace Playhouse.Core.Models.PlaywrightDecorator
{
    public class PageGoToOptionsStrictDecorator
    {
        private const float DEFAULT_TIMEOUT = 30_000F;
        private const WaitUntilState DEFAULT_WAIT_UNTIL = WaitUntilState.Load;

        private readonly PageGotoOptions _options;

        public string? Referer
        {
            get => _options.Referer;
            set => _options.Referer = value;
        }

        public float Timeout
        {
            get => _options.Timeout ??= DEFAULT_TIMEOUT;
            set => _options.Timeout = value;
        }

        public WaitUntilState WaitUntil
        {
            get => _options.WaitUntil ??= DEFAULT_WAIT_UNTIL;
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
            _options.Timeout ??= DEFAULT_TIMEOUT;
            _options.WaitUntil ??= DEFAULT_WAIT_UNTIL;
        }

        public static explicit operator PageGotoOptions(PageGoToOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator);

            return decorator._options; 
        }

        public static implicit operator PageGoToOptionsStrictDecorator(PageGotoOptions? options)
        {
            return new PageGoToOptionsStrictDecorator(options);
        }
    }
}