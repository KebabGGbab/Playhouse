namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public abstract class PageBotAction : BotAction
    {
        public required int Number { get; set; }
    }
}
