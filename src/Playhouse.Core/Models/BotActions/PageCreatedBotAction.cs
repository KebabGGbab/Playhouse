using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.Core.Models.BotActions
{
    public class PageCreatedBotAction : PageBotAction
    {
        public override void Accept(IBotActionVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
