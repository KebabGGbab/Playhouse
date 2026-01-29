using System.Text.Json.Serialization;
using Microsoft.Playwright;

namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class PageBrowserEvent : BrowserEvent
    {
        [JsonIgnore]
        public IPage Page { get; }

        protected PageBrowserEvent(IPage page, string title) : base(title)
        {
            Page = page;
        }
    }
}
