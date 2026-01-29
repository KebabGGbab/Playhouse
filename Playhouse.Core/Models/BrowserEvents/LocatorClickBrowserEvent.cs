using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class LocatorClickBrowserEvent : LocatorBrowserEvent
    {
        public const string NAME = "click";
        public LocatorClickOptions ClickOptions { get; }

        public LocatorClickBrowserEvent(IPage page, string title, LocatorClickOptions? options = null) : base(page, title) 
        {
            ClickOptions = options ?? new LocatorClickOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
