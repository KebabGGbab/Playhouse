namespace Playhouse.Domain.BotActions.Abstractions
{
    public abstract class PageBotAction : BotAction
    {
        public required int Number { get; set; }

        // Конструктор для EntityFramework
        protected PageBotAction()
        {
        }

        protected PageBotAction(BotConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
