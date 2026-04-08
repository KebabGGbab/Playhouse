namespace Playhouse.Core.Models.ConfigurationOptions
{
	public class UserSettings
	{
		public required FileLocationsOptions FileLocations { get; set; }
		public required EntityOptions Entity { get; set;}
		public required PlaywrightOptions Playwright { get; set; }
		public required CultureOptions Culture { get; set; }
	}
}
