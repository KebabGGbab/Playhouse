using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageClosedBrowserEvent : PageBrowserEvent
    {
        public PageCloseOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private PageClosedBrowserEvent()
        {
        }

        public PageClosedBrowserEvent(PageCloseOptionsStrictDecorator? options = null)
        {
            Options = options ?? new PageCloseOptionsStrictDecorator();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
