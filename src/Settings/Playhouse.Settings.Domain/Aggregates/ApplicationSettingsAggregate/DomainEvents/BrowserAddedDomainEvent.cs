using Playhouse.SharedKernel.Domain.Events;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.DomainEvents
{
    public sealed class BrowserAddedDomainEvent : IDomainEvent
    {
        public BrowserType Browser { get; }

        public BrowserAddedDomainEvent(BrowserType browser)
        {
            Browser = browser;
        }
    }
}
