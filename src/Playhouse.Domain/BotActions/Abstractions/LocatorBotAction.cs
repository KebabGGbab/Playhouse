namespace Playhouse.Domain.BotActions.Abstractions
{
    public abstract class LocatorBotAction : PageBotAction
    {
        public LocatorActionData LocatorData { get; } = null!;

        // Конструктор для EntityFramework
        protected LocatorBotAction()
        {
        }

        protected LocatorBotAction(BotConfiguration configuration, LocatorActionData locatorData)
            : base(configuration)
        {
            ArgumentNullException.ThrowIfNull(locatorData);

            LocatorData = locatorData;
        }
    }
}