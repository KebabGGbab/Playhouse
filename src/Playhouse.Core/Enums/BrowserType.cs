using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Playhouse.Core.Enums
{
	[Flags]
    public enum BrowserType
	{
		[Display(Name = "None"), JsonStringEnumMemberName("None")]
		None = 0b_0000,
		[Display(Name = "Chromium"), JsonStringEnumMemberName("Chromium")]
		Chromium = 0b_00001,
		[Display(Name = "WebKit"), JsonStringEnumMemberName("WebKit")]
		WebKit = 0b_0010,
		[Display(Name = "Firefox"), JsonStringEnumMemberName("Firefox")]
		Firefox = 0b_0100,
	}
}
