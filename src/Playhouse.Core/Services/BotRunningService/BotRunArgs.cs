using Jobs.Abstractions;
using Microsoft.Playwright;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotRunArgs : RunArgs
    {
        public IBrowserContext BrowserContext { get; }

        public BotRunArgs(IBrowserContext browserContext)
        {
            ArgumentNullException.ThrowIfNull(browserContext);

            BrowserContext = browserContext;
        }
    }
}
