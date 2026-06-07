using Jobs.Abstractions;
using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotJobContext : JobManagerContext
    {
        public IList<BrowserConfiguration> Profiles { get; }
        public BotConfiguration Bot { get; }

        public BotJobContext(IList<BrowserConfiguration> profiles, BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(profiles);
            ArgumentNullException.ThrowIfNull(bot);

            Profiles = profiles;
            Bot = bot;
        }
    }
}
