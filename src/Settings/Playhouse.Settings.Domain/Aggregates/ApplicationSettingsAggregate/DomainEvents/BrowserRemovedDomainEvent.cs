using Playhouse.SharedKernel.Domain.Events;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.DomainEvents
{
    public sealed class BrowserRemovedDomainEvent : IDomainEvent
    {
        public BrowserType Browser { get; } 

        public BrowserRemovedDomainEvent(BrowserType browser) 
        {
            Browser = browser;
        }
    }
}
