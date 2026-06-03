using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public class BrowserEventHappenedEventArgs : EventArgs
    {
        public BotAction Action { get; }

        public BrowserEventHappenedEventArgs(BotAction action)
        {
            ArgumentNullException.ThrowIfNull(action, nameof(action));

            Action = action;
        }
    }
}
