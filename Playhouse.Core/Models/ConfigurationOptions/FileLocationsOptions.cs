namespace Playhouse.Core.Models.ConfigurationOptions
{
	public class FileLocationsOptions
	{
		public const string Name = "FileLocations";
		public required string Profiles { get; set; }
		public required string Bots { get; set; }
	}
}
