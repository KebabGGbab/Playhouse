using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class BrowserContextClosedBrowserEvent : BrowserContextBrowserEvent
    {
        public BrowserContextCloseOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private BrowserContextClosedBrowserEvent()
        {
        }

        public BrowserContextClosedBrowserEvent(BrowserContextCloseOptionsStrictDecorator? options = null)
        {
            Options = options ?? new BrowserContextCloseOptionsStrictDecorator();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
