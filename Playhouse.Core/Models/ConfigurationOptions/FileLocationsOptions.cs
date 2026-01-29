namespace Playhouse.Core.Models.ConfigurationOptions
{
	public class FileLocationsOptions
	{
		public const string OPTIONSNAME = "FileLocations";

		public required string Profiles { get; set; }
		public required string Bots { get; set; }
	}
}
