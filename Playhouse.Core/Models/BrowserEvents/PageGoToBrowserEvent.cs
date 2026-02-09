using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models.BrowserEvents.OptionsDecorator;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageGoToBrowserEvent : PageBrowserEvent
    {
        public Uri Url { get; init; } = null!;
        public PageGoToOptionsStrictDecorator GotoOptions { get; init; } = null!;

        // Конструктор для EntityFramework
        private PageGoToBrowserEvent()
        {
        }

        public PageGoToBrowserEvent(Uri url, PageGoToOptionsStrictDecorator? options = null)
        {
            Url = url;
            GotoOptions = options ?? new PageGoToOptionsStrictDecorator();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor, nameof(visitor));

            visitor.Visit(this);
        }
    }
}
