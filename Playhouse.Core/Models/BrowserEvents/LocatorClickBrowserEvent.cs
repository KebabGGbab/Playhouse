using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class LocatorClickBrowserEvent : LocatorBrowserEvent
    {
        public const string NAME = "click";

        public LocatorClickOptions ClickOptions { get; init; } = null!;

        // Конструктор для EntityFramework
        private LocatorClickBrowserEvent()
        {
        }

        public LocatorClickBrowserEvent(LocatorClickOptions? options = null)
        {
            ClickOptions = options ?? new LocatorClickOptions();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor, nameof(visitor));

            visitor.Visit(this);
        }
    }
}
