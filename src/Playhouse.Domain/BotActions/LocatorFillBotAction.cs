using Playhouse.Domain.BotActions.Abstractions;
using Playhouse.Domain.PlaywrightDecorator;

namespace Playhouse.Domain.BotActions
{
    public sealed class LocatorFillBotAction : LocatorBotAction
    {
        public const string NAME = "change";

        public string Value { get; set; } = null!;

        public LocatorFillOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private LocatorFillBotAction()
        {
        }

        public LocatorFillBotAction(string value, BotConfiguration configuration, LocatorActionData locatorData, LocatorFillOptionsStrictDecorator? options = null)
            : base(configuration, locatorData)
        {
            ArgumentNullException.ThrowIfNull(value);

            Value = value;
            Options = options ?? new LocatorFillOptionsStrictDecorator();
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
