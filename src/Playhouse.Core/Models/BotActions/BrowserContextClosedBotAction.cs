using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BotActions
{
    public class BrowserContextClosedBotAction : BrowserContextBotAction
    {
        public BrowserContextCloseOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private BrowserContextClosedBotAction()
        {
        }

        public BrowserContextClosedBotAction(BrowserContextCloseOptionsStrictDecorator? options = null)
        {
            Options = options ?? new BrowserContextCloseOptionsStrictDecorator();
        }

        public override void Accept(IBotActionVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
