using Playhouse.Domain.BotActions.Abstractions;
using Playhouse.Domain.PlaywrightDecorator;

namespace Playhouse.Domain.BotActions
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
