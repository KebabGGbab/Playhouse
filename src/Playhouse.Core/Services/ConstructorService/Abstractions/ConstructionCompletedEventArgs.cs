using Playhouse.Domain;

namespace Playhouse.Core.Services.ConstructorService.Abstractions
{
    public class ConstructionCompletedEventArgs : EventArgs
    {
        public BotConfiguration Bot { get; set; }

        public ConstructionCompletedEventArgs(BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));

            Bot = bot;
        }
    }
}
