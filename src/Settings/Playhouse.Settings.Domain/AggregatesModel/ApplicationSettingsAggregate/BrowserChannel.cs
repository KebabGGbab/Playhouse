using Ardalis.SmartEnum;

namespace Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate
{
    public sealed class BrowserChannel : SmartEnum<BrowserChannel>
    {
        public static readonly BrowserChannel Chromium = new("chromium", 1, BrowserType.Chromium);
        public static readonly BrowserChannel Chrome = new("chrome", 2, BrowserType.Chromium);
        public static readonly BrowserChannel ChromeBeta = new("chrome-beta", 3, BrowserType.Chromium);
        public static readonly BrowserChannel ChromeDev = new("chrome-dev", 4, BrowserType.Chromium);
        public static readonly BrowserChannel ChromeCanary = new("chrome-canary", 5, BrowserType.Chromium);
        public static readonly BrowserChannel Msedge = new("msedge", 6, BrowserType.Chromium);
        public static readonly BrowserChannel MsedgeBeta = new("msedge-beta", 7, BrowserType.Chromium);
        public static readonly BrowserChannel MsedgeDev = new("msedge-dev", 8, BrowserType.Chromium);
        public static readonly BrowserChannel MsedgeCanary = new("msedge-canary", 9, BrowserType.Chromium);

        public BrowserType OwnerBrowser { get; }

        private BrowserChannel(string name, int value, BrowserType browser)
            : base(name, value)
        {
            OwnerBrowser = browser;
        }
    }
}
