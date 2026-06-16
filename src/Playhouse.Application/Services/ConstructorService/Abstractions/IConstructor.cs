namespace Playhouse.Application.Services.ConstructorService.Abstractions
{
    public interface IConstructor
    {
        event EventHandler<IConstructor, BrowserEventHappenedEventArgs> ActionHappend;
        event EventHandler<IConstructor, ConstructionCompletedEventArgs> ConstructionCompleted;

        Task StartConstructionAsync();

        Task CompleteConstructionAsync();
    }
}
