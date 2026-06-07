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
