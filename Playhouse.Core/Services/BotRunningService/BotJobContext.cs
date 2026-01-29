using Jobs.Abstractions;
using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotJobContext : JobManagerContext
    {
        public IList<BrowserProfile> Profiles { get; set; }
        public BotInfo BotInfo { get; set; }

        public BotJobContext(IList<BrowserProfile> profiles, BotInfo botInfo)
        {
            ArgumentNullException.ThrowIfNull(profiles, nameof(profiles));
            ArgumentNullException.ThrowIfNull(botInfo, nameof(botInfo));

            Profiles = profiles;
            BotInfo = botInfo;
        }
    }
}
