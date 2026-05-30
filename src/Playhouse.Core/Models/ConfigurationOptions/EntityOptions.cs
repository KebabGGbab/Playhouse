namespace Playhouse.Core.Models.ConfigurationOptions
{
	public class EntityOptions
	{
		public const string OPTIONSNAME = "Entity";

		public required string DefaultProfileName { get; set; }
		public required string DefaultBotName { get; set; }
	}
}
