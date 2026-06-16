using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.Domain.BotActions
{
    public sealed class BrowserContextCreatedBotAction : BrowserContextBotAction
    {
        // Конструктор для EntityFramework
        private BrowserContextCreatedBotAction()
        {
        }

        public BrowserContextCreatedBotAction(BotConfiguration configuration)
            : base(configuration)
        {
        }

        public override T Accept<T>(IBotActionVisitor<T> visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            return visitor.Visit(this);
        }

        public override async Task Accept(IBotActionAsyncVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            await visitor.VisitAsync(this)
                .ConfigureAwait(false);
        }
    }
}
