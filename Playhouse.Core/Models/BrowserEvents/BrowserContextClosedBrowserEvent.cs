using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class BrowserContextClosedBrowserEvent : BrowserContextBrowserEvent
    {
        public BrowserContextCloseOptions CloseOptions { get; init; } = null!;

        // Конструктор для EntityFramework
        private BrowserContextClosedBrowserEvent()
        {
        }

        public BrowserContextClosedBrowserEvent(BrowserContextCloseOptions? options = null)
        {
            CloseOptions = options ?? new BrowserContextCloseOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor, nameof(visitor));

            visitor.Visit(this);
        }
    }
}
