using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageGoToBrowserEvent : PageBrowserEvent
    {
        public string Url { get; set; }
        public PageGotoOptions GotoOptions { get; }

        public PageGoToBrowserEvent(IPage page, string title, string url, PageGotoOptions? options = null) : base(page, title)
        {
            Url = url;
            GotoOptions = options ?? new PageGotoOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
