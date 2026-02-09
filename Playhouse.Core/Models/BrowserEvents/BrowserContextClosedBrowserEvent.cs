using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models.BrowserEvents.OptionsDecorator;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class BrowserContextClosedBrowserEvent : BrowserContextBrowserEvent
    {
        public BrowserContextCloseOptionsStrictDecorator CloseOptions { get; init; } = null!;

        // Конструктор для EntityFramework
        private BrowserContextClosedBrowserEvent()
        {
        }

        public BrowserContextClosedBrowserEvent(BrowserContextCloseOptionsStrictDecorator? options = null)
        {
            CloseOptions = options ?? new BrowserContextCloseOptionsStrictDecorator();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor, nameof(visitor));

            visitor.Visit(this);
        }
    }
}
