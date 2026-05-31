using Microsoft.Playwright;

namespace Playhouse.Core.Models.PlaywrightDecorator
{
    public class PageCloseOptionsStrictDecorator
    {
        private const bool DEFAULT_RUN_BEFORE_UNLOAD = false;

        private readonly PageCloseOptions _options;

        public bool RunBeforeUnload
        {
            get => _options.RunBeforeUnload ??= DEFAULT_RUN_BEFORE_UNLOAD;
            set => _options.RunBeforeUnload = value;
        }

        public string? Reason
        {
            get => _options.Reason;
            set => _options.Reason = value;
        }

        // Конструктор для EntityFramework
        private PageCloseOptionsStrictDecorator()
            : this(null)
        {
        }

        public PageCloseOptionsStrictDecorator(PageCloseOptions? options = null)
        {
            _options = options ?? new PageCloseOptions();
            _options.RunBeforeUnload ??= DEFAULT_RUN_BEFORE_UNLOAD;
        }

        public static explicit operator PageCloseOptions(PageCloseOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator);

            return decorator._options;
        }

        public static implicit operator PageCloseOptionsStrictDecorator(PageCloseOptions options)
        {
            return new PageCloseOptionsStrictDecorator(options);
        }
    }
}
