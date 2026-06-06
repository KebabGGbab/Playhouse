using Playhouse.Core.Services.BotConstructorService.Abstractions;

namespace Playhouse.Core.Services.ConstructorService.Abstractions
{
    public interface IConstructor
    {
        event EventHandler<IConstructor, BrowserEventHappenedEventArgs> ActionHappend;
        event EventHandler<IConstructor, ConstructionCompletedEventArgs> ConstructionCompleted;

        Task StartConstructorAsync();
    }
}
