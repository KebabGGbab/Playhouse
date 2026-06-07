namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public interface IBotActionVisitor<T>
    {
        T Visit(BrowserContextCreatedBotAction action);
        T Visit(BrowserContextClosedBotAction action);
        T Visit(PageCreatedBotAction action);
        T Visit(PageClosedBotAction action);
        T Visit(PageGoToBotAction action);
        T Visit(LocatorClickBotAction action);
        T Visit(LocatorFillBotAction action);
    }
}
