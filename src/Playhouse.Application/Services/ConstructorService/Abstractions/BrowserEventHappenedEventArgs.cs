using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.Application.Services.ConstructorService.Abstractions
{
    public class BrowserEventHappenedEventArgs : EventArgs
    {
        public BotAction Action { get; }

        public BrowserEventHappenedEventArgs(BotAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            Action = action;
        }
    }
}
