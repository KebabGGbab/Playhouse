namespace Playhouse.Domain.BotActions.Abstractions
{
    public interface IBotActionAsyncVisitor
    {
        Task VisitAsync(BrowserContextCreatedBotAction action);
        Task VisitAsync(BrowserContextClosedBotAction action);
        Task VisitAsync(PageCreatedBotAction action);
        Task VisitAsync(PageClosedBotAction action);
        Task VisitAsync(PageGoToBotAction action);
        Task VisitAsync(LocatorClickBotAction action);
        Task VisitAsync(LocatorFillBotAction action);
    }
}
