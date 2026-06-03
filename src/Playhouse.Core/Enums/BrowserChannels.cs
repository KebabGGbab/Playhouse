using System.ComponentModel;

namespace Playhouse.Core.Enums
{
	public enum BrowserChannels
	{
		[Description("none")]
		None = 0,

		[Description("chromium")]
		Chromium = 1,

		[Description("chrome")]
		Chrome = 2,

		[Description("chrome-beta")]
		ChromeBeta = 3,

		[Description("chrome-canary")]
		ChromeCanary = 4,

		[Description("chrome-dev")]
		ChromeDev = 5,

		[Description("msedge")]
		Msedge = 6,

		[Description("msedge-beta")]
		MsedgeBeta = 7,

		[Description("msedge-canary")]
		MsedgeCanary = 8,

		[Description("msedge-dev")]
		MsedgeDev = 9,
	}
}
