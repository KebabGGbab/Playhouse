namespace Playhouse.Domain.BotActions.Abstractions
{
    public abstract class BrowserContextBotAction : BotAction
    {
        public required int Number { get; set; }

        // Конструктор для EntityFramework
        protected BrowserContextBotAction() 
        {
        }

        protected BrowserContextBotAction(BotConfiguration configuration) 
            : base(configuration) 
        { 
        }
    }
}
