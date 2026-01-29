using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class BrowserContextClosedBrowserEvent : BrowserContextBrowserEvent
    {
        public BrowserContextCloseOptions CloseOptions { get; }
         
        public BrowserContextClosedBrowserEvent(IBrowserContext browserContext, string title, BrowserContextCloseOptions? options = null) : base(browserContext, title)
        {
            CloseOptions = options ?? new BrowserContextCloseOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
