using Playhouse.Core.Enums;

namespace Playhouse.Core.Models.ConfigurationOptions
{
	public class PlaywrightOptions
	{
		public const string Name = "Playwright";

		public Channel Channels { get; set; }
		public BrowserType BrowserTypes { get; set; }
	}
}
