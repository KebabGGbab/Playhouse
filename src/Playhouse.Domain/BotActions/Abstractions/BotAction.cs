namespace Playhouse.Domain.BotActions.Abstractions
{
    public abstract class BotAction
    {
        public int Id { get; private set; }

        public required int ActionNumber { get; init; }

        public BotConfiguration Bot { get; } = null!;

        // Конструктор для EntityFramework
        protected BotAction()
        { 
        } 

        protected BotAction(BotConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            Bot = configuration;
        }

        public abstract T Accept<T>(IBotActionVisitor<T> visitor);

        public abstract Task Accept(IBotActionAsyncVisitor visitor);
    }
}
