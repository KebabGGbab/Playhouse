using Playhouse.Core.Models;

namespace Playhouse.Core.Services.ConstructorService.Abstractions
{
    public interface IConstructorFactory
    {
        IConstructor Create(BrowserConfiguration profile, BotConfiguration bot);
    }
}
