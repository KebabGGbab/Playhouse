using Ardalis.SmartEnum;

namespace Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate
{
    public sealed class BrowserType : SmartEnum<BrowserType>
    {
        public static readonly BrowserType Chromium = new(nameof(Chromium), 1);
        public static readonly BrowserType WebKit = new(nameof(WebKit), 2);
        public static readonly BrowserType Firefox = new(nameof(Firefox), 3);

        private BrowserType(string name, int value)
            : base(name, value)
        {
        }
    }
}
