using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageClosedBrowserEvent : PageBrowserEvent
    {
        public PageCloseOptions CloseOptions { get; init; } = null!;

        // Конструктор для EntityFramework
        private PageClosedBrowserEvent()
        {
        }

        public PageClosedBrowserEvent(PageCloseOptions? options = null)
        { 
            CloseOptions = options ?? new PageCloseOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor, nameof(visitor));

            visitor.Visit(this);
        }
    }
}
