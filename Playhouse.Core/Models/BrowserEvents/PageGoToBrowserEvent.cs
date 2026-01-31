using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageGoToBrowserEvent : PageBrowserEvent
    {
        public Uri Url { get; init; } = null!;
        public PageGotoOptions GotoOptions { get; init; } = null!;

        // Конструктор для EntityFramework
        private PageGoToBrowserEvent()
        {
        }

        public PageGoToBrowserEvent(Uri url, PageGotoOptions? options = null)
        {
            Url = url;
            GotoOptions = options ?? new PageGotoOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor, nameof(visitor));

            visitor.Visit(this);
        }
    }
}
