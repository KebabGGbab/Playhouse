using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public interface IBotConstructorFactory
    {
        IBotConstructor Create(BrowserProfile profile, BotInfo bot);
    }
}
