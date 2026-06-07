using Microsoft.Playwright;

namespace Playhouse.Core.Models.PlaywrightDecorator
{
    public class LocatorFillOptionsStrictDecorator
    {
        private const bool DEFAULT_FORCE = false;
        private const float DEFAULT_TIMEOUT = 30_000;

        private readonly LocatorFillOptions _options;

        public bool Force
        {
            get => _options.Force ??= DEFAULT_FORCE;
            set => _options.Force = value;
        }

        public float Timeout
        {
            get => _options.Timeout ??= DEFAULT_TIMEOUT;
            set => _options.Timeout = value;
        }

        // Конструктор для EntityFramework
        private LocatorFillOptionsStrictDecorator() 
            : this(null)
        {
        }

        public LocatorFillOptionsStrictDecorator(LocatorFillOptions? options = null)
        {
            _options = options ?? new LocatorFillOptions();
            _options.Force ??= DEFAULT_FORCE;
            _options.Timeout ??= DEFAULT_TIMEOUT;
        }

        public static implicit operator LocatorFillOptions(LocatorFillOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator);

            return decorator._options;
        }

        public static explicit operator LocatorFillOptionsStrictDecorator(LocatorFillOptions? options)
        {
            return new LocatorFillOptionsStrictDecorator(options);
        }
    }
}
