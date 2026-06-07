using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BotActions
{
    public class LocatorClickBotAction : LocatorBotAction
    {
        public const string NAME = "click";

        public LocatorClickOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private LocatorClickBotAction()
        {
        }

        public LocatorClickBotAction(BotConfiguration configuration, LocatorActionData locatorData, LocatorClickOptionsStrictDecorator? options = null)
            : base(configuration, locatorData)
        {
            Options = options ?? new LocatorClickOptionsStrictDecorator();
        }

        public override void Accept(IBotActionVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
