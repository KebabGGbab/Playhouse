using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.Core.Services.ConstructorService.Abstractions
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
