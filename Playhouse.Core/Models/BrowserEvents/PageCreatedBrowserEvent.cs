using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageCreatedBrowserEvent : PageBrowserEvent
    {
        public PageCreatedBrowserEvent(IPage page, string title) : base(page, title) { }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
