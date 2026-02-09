using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models.BrowserEvents.OptionsDecorator;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class LocatorClickBrowserEvent : LocatorBrowserEvent
    {
        public const string NAME = "click";

        public LocatorClickOptionsStrictDecorator ClickOptions { get; init; } = null!;

        // Конструктор для EntityFramework
        private LocatorClickBrowserEvent()
        {
        }

        public LocatorClickBrowserEvent(LocatorClickOptionsStrictDecorator? options = null)
        {
            ClickOptions = options ?? new LocatorClickOptionsStrictDecorator();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor, nameof(visitor));

            visitor.Visit(this);
        }
    }
}
