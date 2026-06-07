namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public abstract class LocatorBotAction : PageBotAction
    {
        public string? Text { get; set; }
        // Конструктор для EntityFramework
        protected LocatorBotAction()
        {
    }

        protected LocatorBotAction(BotConfiguration configuration)
            : base(configuration)
        {
        }
    }
}