using System.Globalization;
using Ardalis.SmartEnum;

namespace Playhouse.Core.Models
{
    public sealed class BrowserTypes : SmartEnum<BrowserTypes>
    {
        private readonly HashSet<BrowserChannels> _channels;

        public static readonly BrowserTypes Chromium = new("Chromium", 1);
        public static readonly BrowserTypes Firefox = new("Firefox", 2);
        public static readonly BrowserTypes WebKit = new("WebKit", 3);

        public IReadOnlyCollection<BrowserChannels> Channels { get; }

        public string CliName { get; }

        private BrowserTypes(string name, int value) : base(name, value)
        {
            _channels = [];
            Channels = _channels.AsReadOnly();
            CliName = name.ToLower(CultureInfo.InvariantCulture);
        }

        internal void AddChannel(BrowserChannels channel)
        {
            ArgumentNullException.ThrowIfNull(channel);

            _channels.Add(channel);
        }
    }
}
