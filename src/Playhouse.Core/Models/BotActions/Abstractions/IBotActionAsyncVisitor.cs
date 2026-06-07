namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public interface IBotActionAsyncVisitor
    {
        Task VisitAsync(BrowserContextClosedBotAction action);
        Task VisitAsync(PageCreatedBotAction action);
        Task VisitAsync(PageClosedBotAction action);
        Task VisitAsync(PageGoToBotAction action);
        Task VisitAsync(LocatorClickBotAction action);
    }
}
