using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.Core.Models.BotActions
{
    public class PageCreatedBotAction : PageBotAction
    {
        // Конструктор для EntityFramework
        private PageCreatedBotAction() 
        {
        }

        public PageCreatedBotAction(BotConfiguration configuration)
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
