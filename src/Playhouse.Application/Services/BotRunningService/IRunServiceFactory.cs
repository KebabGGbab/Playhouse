using Playhouse.Domain;

namespace Playhouse.Application.Services.BotRunningService
{
    public interface IRunServiceFactory
    {
        IRunService Create(BotConfiguration bot, IEnumerable<BrowserConfiguration> browsers);
    }
}
