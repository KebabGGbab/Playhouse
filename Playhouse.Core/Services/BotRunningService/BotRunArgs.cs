using Jobs.Abstractions;
using Microsoft.Playwright;
using PlayhouseShare;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotRunArgs : RunArgs
    {
        public Bot Bot { get; set; }
        public IBrowserContext BrowserContext { get; set; }

        public BotRunArgs(Bot bot, IBrowserContext browserContext)
        {
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));
            ArgumentNullException.ThrowIfNull(browserContext, nameof(browserContext));

            Bot = bot;
            BrowserContext = browserContext;
        }
    }
}
