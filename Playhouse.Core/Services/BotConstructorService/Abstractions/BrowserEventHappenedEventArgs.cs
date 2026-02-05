using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Services.BotConstructorService.Abstractions
{
    public class BrowserEventHappenedEventArgs : EventArgs
    {
        public BrowserEvent BrowserEventHappened { get; }

        public BrowserEventHappenedEventArgs(BrowserEvent browserevent)
        {
            ArgumentNullException.ThrowIfNull(browserevent, nameof(browserevent));

            BrowserEventHappened = browserevent;
        }
    }
}
