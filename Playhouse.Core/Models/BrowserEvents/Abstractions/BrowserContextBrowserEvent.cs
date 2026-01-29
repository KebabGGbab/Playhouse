using System.Text.Json.Serialization;
using Microsoft.Playwright;

namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class BrowserContextBrowserEvent : BrowserEvent
    {
        [JsonIgnore]
        public IBrowserContext BrowserContext { get; }

        protected BrowserContextBrowserEvent(IBrowserContext browserContext, string title) : base(title)
        {
            BrowserContext = browserContext;
        }
    }
}
