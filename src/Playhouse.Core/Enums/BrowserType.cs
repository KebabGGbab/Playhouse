using System.ComponentModel;

namespace Playhouse.Core.Enums
{
	[Flags]
    public enum BrowserType
	{
		[Description("None")]
		None = 0,

		[Description("Chromium")]
		Chromium = 1,

		[Description("WebKit")]
		WebKit = 2,

		[Description("Firefox")]
		Firefox = 3,
	}
}
