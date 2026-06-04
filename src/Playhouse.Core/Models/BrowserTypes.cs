using Ardalis.SmartEnum;

namespace Playhouse.Core.Models
{
    public sealed class BrowserTypes : SmartEnum<BrowserTypes>
    {
        private readonly HashSet<BrowserChannels> _channels;

        public static readonly BrowserTypes Chromium = new("chromium", 1);
        public static readonly BrowserTypes Firefox = new("firefox", 2);
        public static readonly BrowserTypes WebKit = new("webKit", 3);

        public IReadOnlyCollection<BrowserChannels> Channels { get; }

        public BrowserTypes(string name, int value) : base(name, value)
        {
            _channels = [];
            Channels = _channels.AsReadOnly();
        }

        internal void AddChannel(BrowserChannels channel)
        {
            ArgumentNullException.ThrowIfNull(channel);

            _channels.Add(channel);
        }
    }
}
