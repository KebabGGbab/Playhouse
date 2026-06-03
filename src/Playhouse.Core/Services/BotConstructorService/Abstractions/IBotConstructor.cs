namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public interface IBotConstructor
    {
        event EventHandler<IBotConstructor, BrowserEventHappenedEventArgs> ActionHappend;
        event EventHandler<IBotConstructor, BotConstructionCompletedEventArgs> ConstructionCompleted;

        Task StartConstructorAsync();
    }
}
