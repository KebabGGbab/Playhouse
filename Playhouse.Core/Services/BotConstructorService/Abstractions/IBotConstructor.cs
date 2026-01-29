using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public interface IBotConstructor
    {
        event EventHandler<BrowserEvent> BrowserEventReceived;
        event EventHandler ConstructionCompleted;

        Task StartConstructorAsync(BrowserProfile profile, BotInfo bot);
    }
}
