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
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
