using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageClosedBrowserEvent : PageBrowserEvent
    {
        public PageCloseOptions CloseOptions { get; }

        public PageClosedBrowserEvent(IPage page, string title, PageCloseOptions? options = null) : base(page, title)
        { 
            CloseOptions = options ?? new PageCloseOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
