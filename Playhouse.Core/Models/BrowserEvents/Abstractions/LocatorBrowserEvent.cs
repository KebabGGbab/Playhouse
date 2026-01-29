using Microsoft.Playwright;

namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class LocatorBrowserEvent : PageBrowserEvent
    {
        public string? Text { get; set; }

        protected LocatorBrowserEvent(IPage page, string title) : base(page, title) { }
    }
}