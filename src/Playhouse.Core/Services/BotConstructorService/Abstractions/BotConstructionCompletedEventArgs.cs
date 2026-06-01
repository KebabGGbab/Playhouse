using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public class BotConstructionCompletedEventArgs : EventArgs
    {
        public BotConfiguration Bot { get; set; }

        public BotConstructionCompletedEventArgs(BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));

            Bot = bot;
        }
    }
}
