using Playhouse.Core.Enums;

namespace Playhouse.Core.Models.ConfigurationOptions
{
	public class PlaywrightOptions
	{
		public const string OPTIONSNAME = "Playwright";

		public BrowserChannels Channels { get; set; }
		public BrowserType BrowserTypes { get; set; }
	}
}
