using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotRunningService
{
    public interface IRunServiceFactory
    {
        IRunService Create(BotConfiguration bot, IEnumerable<BrowserConfiguration> browsers);
    }
}
