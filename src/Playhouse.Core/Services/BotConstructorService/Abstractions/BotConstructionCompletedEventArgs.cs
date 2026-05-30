using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public class BotConstructionCompletedEventArgs : EventArgs
    {
        public BotInfo BotInfo { get; set; }

        public BotConstructionCompletedEventArgs(BotInfo botInfo)
        {
            ArgumentNullException.ThrowIfNull(botInfo, nameof(botInfo));

            BotInfo = botInfo;
        }
    }
}
