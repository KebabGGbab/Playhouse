using Playhouse.Domain;

namespace Playhouse.Application.Services.ConstructorService.Abstractions
{
    public interface IConstructorFactory
    {
        IConstructor Create(BrowserConfiguration profile, BotConfiguration bot);
    }
}
