using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class PageGoToBrowserEvent : PageBrowserEvent
    {
        public string Url 
        { 
            get;
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);

                field = value;
            }
        } = null!;

        public PageGoToOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private PageGoToBrowserEvent()
        {
        }

        public PageGoToBrowserEvent(string url, PageGoToOptionsStrictDecorator? options = null)
        {
            Url = url;
            Options = options ?? new PageGoToOptionsStrictDecorator();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
