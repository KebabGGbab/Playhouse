using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BotActions
{
    public class PageClosedBotAction : PageBotAction
    {
        public PageCloseOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private PageClosedBotAction()
        {
        }

        public PageClosedBotAction(BotConfiguration configuration, PageCloseOptionsStrictDecorator? options = null)
            : base(configuration)
        {
            Options = options ?? new PageCloseOptionsStrictDecorator();
        }

        public override void Accept(IBotActionVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
