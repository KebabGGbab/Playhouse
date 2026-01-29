namespace Playhouse.Core.Models.ConfigurationOptions
{
	public sealed class UserSettings
	{
		public required FileLocationsOptions FileLocations { get; init; }
		public required EntityOptions Entity { get; init;}
		public required PlaywrightOptions Playwright { get; init; }
		public required ViewOptions View { get; init; }
	}
}
