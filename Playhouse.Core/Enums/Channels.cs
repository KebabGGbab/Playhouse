using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Playhouse.Core.Enums
{
	[Flags]
	public enum Channel
	{
		[Display(Name = "none"), JsonStringEnumMemberName("none")]
		None = 0b_0000_0000_0000,
		[Display(Name = "chromium"), JsonStringEnumMemberName("chromium")]
		Chromium = 0b_0000_0000_0001,
		[Display(Name = "chrome"), JsonStringEnumMemberName("chrome")]
		Chrome = 0b_0000_0000_0010,
		[Display(Name = "chrome-beta"), JsonStringEnumMemberName("chrome-beta")]
		ChromeBeta = 0b_0000_0000_0100,
		[Display(Name = "chrome-canary"), JsonStringEnumMemberName("chrome-canary")]
		ChromeCanary = 0b_0000_0000_1000,
		[Display(Name = "chrome-dev"), JsonStringEnumMemberName("chrome-dev")]
		ChromeDev = 0b_0000_0001_0000,
		[Display(Name = "msedge"), JsonStringEnumMemberName("msedge")]
		Msedge = 0b_0000_0010_0000,
		[Display(Name = "msedge-beta"), JsonStringEnumMemberName("msedge-beta")]
		MsedgeBeta = 0b_0000_0100_0000,
		[Display(Name = "msedge-canary"), JsonStringEnumMemberName("msedge-canary")]
		MsedgeCanary = 0b_0000_1000_0000,
		[Display(Name = "msedge-dev"), JsonStringEnumMemberName("msedge-dev")]
		MsedgeDev = 0b_0001_0000_0000,
	}
}
