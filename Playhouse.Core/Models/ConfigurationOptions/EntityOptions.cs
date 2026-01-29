namespace Playhouse.Core.Models.ConfigurationOptions
{
	public class EntityOptions
	{
		public const string Name = "Entity";

		public required string DefaultProfileName { get; set; }
		public required string DefaultBotName { get; set; }
		public required int ProfileLastId { get; set; }
		public required int BotLastId { get; set; }
	}
}
