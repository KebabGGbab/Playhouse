using Microsoft.Playwright;

namespace Playhouse.Core.Models.PlaywrightDecorator
{
    public class LocatorClickOptionsStrictDecorator
    {
        private const MouseButton DEFAULT_BUTTON = MouseButton.Left;
        private const int DEFAULT_CLICK_COUNT = 1;
        private const float DEFAULT_DELAY = 0F;
        private const bool DEFAULT_FORCE = false;
        private const int DEFAULT_STEPS = 1;
        private const float DEFAULT_TIMEOUT = 30_000F;
        private const bool DEFAULT_TRIAL = false;    

        private readonly LocatorClickOptions _options;
        private readonly HashSet<KeyboardModifier> _modifiers;

        public MouseButton Button
        {
            get => _options.Button ??= DEFAULT_BUTTON;
            set => _options.Button = value;
        }

        public int ClickCount
        {
            get => _options.ClickCount ??= DEFAULT_CLICK_COUNT;
            set => _options.ClickCount = value;
        }

        public float Delay
        {
            get => _options.Delay ??= DEFAULT_DELAY;
            set => _options.Delay = value;
        }

        public bool Force
        {
            get => _options.Force ??= DEFAULT_FORCE;
            set => _options.Force = value;
        }

        public ISet<KeyboardModifier> Modifiers => _modifiers;

        public int Steps
        {
            get => _options.Steps ??= DEFAULT_STEPS;
            set => _options.Steps = value;
        }

        public float Timeout
        {
            get => _options.Timeout ??= DEFAULT_TIMEOUT;
            set => _options.Timeout = value;
        }

        public bool Trial
        {
            get => _options.Trial ??= DEFAULT_TRIAL;
            set => _options.Trial = value;
        }

        // Конструктор для EntityFramework
        private LocatorClickOptionsStrictDecorator()
            : this(null)
        {
        }

        public LocatorClickOptionsStrictDecorator(LocatorClickOptions? options = null)
        {
            _options = options ??= new LocatorClickOptions();
            _options.Button ??= DEFAULT_BUTTON;
            _options.ClickCount ??= DEFAULT_CLICK_COUNT;
            _options.Delay ??= DEFAULT_DELAY;
            _options.Force ??= DEFAULT_FORCE;
            _modifiers = new(_options.Modifiers ??= []);
            _options.Modifiers = _modifiers;
            _options.Steps ??= DEFAULT_STEPS;
            _options.Timeout ??= DEFAULT_TIMEOUT;
            _options.Trial ??= DEFAULT_TRIAL;
        }

        public static explicit operator LocatorClickOptions(LocatorClickOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator);

            return decorator._options;
        }

        public static implicit operator LocatorClickOptionsStrictDecorator(LocatorClickOptions? options)
        {
            return new LocatorClickOptionsStrictDecorator(options);
        }
    }
}
