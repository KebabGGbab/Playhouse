namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public interface IBotActionVisitor
    {
        void Visit(PageCreatedBotAction action);
        void Visit(PageClosedBotAction action);
        void Visit(PageGoToBotAction action);
        void Visit(BrowserContextClosedBotAction action);
        void Visit(LocatorClickBotAction action);
    }
}
