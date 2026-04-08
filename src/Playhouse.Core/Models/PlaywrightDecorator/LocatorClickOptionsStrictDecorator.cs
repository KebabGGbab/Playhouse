using Microsoft.Playwright;

namespace Playhouse.Core.Models.PlaywrightDecorator
{
    public class LocatorClickOptionsStrictDecorator
    {
        private const MouseButton DEFAULTBUTTON = MouseButton.Left;
        private const int DEFAULTCLICKCOUNT = 1;
        private const float DEFAULTDELAY = 0;
        private const bool DEFAULTFORCE = false;
        private const int DEFAULTSTEPS = 1;
        private const int DEFAULTTIMEOUT = 30000;
        private const bool DEFAULTTRIAL = false;    

        private readonly LocatorClickOptions _options;

        public MouseButton Button
        {
            get => _options.Button ??= DEFAULTBUTTON;
            set => _options.Button = value;
        }

        public int ClickCount
        {
            get => _options.ClickCount ??= DEFAULTCLICKCOUNT;
            set => _options.ClickCount = value;
        }

        public float Delay
        {
            get => _options.Delay ??= DEFAULTDELAY;
            set => _options.Delay = value;
        }

        public bool Force
        {
            get => _options.Force ??= DEFAULTFORCE;
            set => _options.Force = value;
        }

        public IEnumerable<KeyboardModifier>? Modifiers { get; } = [];

        public Position? Position
        {
            get => _options.Position;
            set => _options.Position = value;
        }

        public int Steps
        {
            get => _options.Steps ??= DEFAULTSTEPS;
            set => _options.Steps = value;
        }

        public float Timeout
        {
            get => _options.Timeout ??= DEFAULTTIMEOUT;
            set => _options.Timeout = value;
        }

        public bool Trial
        {
            get => _options.Trial ??= DEFAULTTRIAL;
            set => _options.Trial = value;
        }

        // Конструктор для EntityFramework
        private LocatorClickOptionsStrictDecorator()
            : this(null)
        {
        }

        public LocatorClickOptionsStrictDecorator(LocatorClickOptions? options = null)
        {
            _options = options ?? new LocatorClickOptions();
            _options.Button ??= DEFAULTBUTTON;
            _options.ClickCount ??= DEFAULTCLICKCOUNT;
            _options.Delay ??= DEFAULTDELAY;
            _options.Force ??= DEFAULTFORCE;
            _options.Steps ??= DEFAULTSTEPS;
            _options.Timeout ??= DEFAULTTIMEOUT;
            _options.Trial ??= DEFAULTTRIAL;
        }

        public static explicit operator LocatorClickOptions(LocatorClickOptionsStrictDecorator decorator)
        {
            ArgumentNullException.ThrowIfNull(decorator, nameof(decorator));

            return decorator._options;
        }

        public static explicit operator LocatorClickOptionsStrictDecorator(LocatorClickOptions options)
        {
            return new LocatorClickOptionsStrictDecorator(options);
        }
    }
}
