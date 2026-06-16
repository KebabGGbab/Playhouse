using Playhouse.Domain;

namespace Playhouse.Core.Services.ConstructorService.Abstractions
{
    public interface IConstructorFactory
    {
        IConstructor Create(BrowserConfiguration profile, BotConfiguration bot);
    }
}
