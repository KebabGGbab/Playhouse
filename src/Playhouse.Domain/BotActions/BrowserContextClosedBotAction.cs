using Playhouse.Domain.BotActions.Abstractions;
using Playhouse.Domain.PlaywrightDecorator;

namespace Playhouse.Domain.BotActions
{
    public class BrowserContextClosedBotAction : BrowserContextBotAction
    {
        public BrowserContextCloseOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private BrowserContextClosedBotAction()
        {
        }

        public BrowserContextClosedBotAction(BotConfiguration configuration, BrowserContextCloseOptionsStrictDecorator? options = null)
            : base(configuration)
        {
            Options = options ?? new BrowserContextCloseOptionsStrictDecorator();
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
