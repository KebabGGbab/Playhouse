using System.Globalization;
using Ardalis.SmartEnum;

namespace Playhouse.Domain
{
    public sealed class BrowserChannels : SmartEnum<BrowserChannels>
    {
        public static readonly BrowserChannels Chromium = new("Chromium", 1, BrowserTypes.Chromium);
        public static readonly BrowserChannels Chrome = new("Chrome", 2, BrowserTypes.Chromium);
        public static readonly BrowserChannels ChromeBeta = new("Chrome Beta", 3, BrowserTypes.Chromium);
        public static readonly BrowserChannels ChromeCanary = new("Chrome Canary", 4, BrowserTypes.Chromium);
        public static readonly BrowserChannels ChromeDev = new("Chrome Dev", 5, BrowserTypes.Chromium);
        public static readonly BrowserChannels MsEdge = new("MS Edge", 6, BrowserTypes.Chromium);
        public static readonly BrowserChannels MsEdgeBeta = new("MS Edge Beta", 7, BrowserTypes.Chromium);
        public static readonly BrowserChannels MsEdgeCanary = new("MS Edge Canary", 8, BrowserTypes.Chromium);
        public static readonly BrowserChannels MsEdgeDev = new("MS Edge Dev", 9, BrowserTypes.Chromium);

        public BrowserTypes Owner { get; }

        public string CliName { get; }

        private BrowserChannels(string name, int value, BrowserTypes owner) : base(name, value)
        {
            Owner = owner;
            Owner.AddChannel(this);
            CliName = name.ToLower(CultureInfo.InvariantCulture);
        }
    }
}
