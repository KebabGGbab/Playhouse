using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public interface IBotConstructorFactory
    {
        IBotConstructor Create(BrowserConfiguration profile, BotConfiguration bot);
    }
}
