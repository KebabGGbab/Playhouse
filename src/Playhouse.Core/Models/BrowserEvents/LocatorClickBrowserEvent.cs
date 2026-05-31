using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BrowserEvents
{
    public class LocatorClickBrowserEvent : LocatorBrowserEvent
    {
        public const string NAME = "click";

        public LocatorClickOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private LocatorClickBrowserEvent()
        {
        }

        public LocatorClickBrowserEvent(LocatorClickOptionsStrictDecorator? options = null)
        {
            Options = options ?? new LocatorClickOptionsStrictDecorator();
        }

        public override void Accept(IBrowserEventVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
