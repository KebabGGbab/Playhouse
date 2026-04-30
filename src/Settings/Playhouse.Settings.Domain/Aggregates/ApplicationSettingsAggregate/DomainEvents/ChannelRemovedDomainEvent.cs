using Playhouse.SharedKernel.Domain.Events;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.DomainEvents
{
    public sealed class ChannelRemovedDomainEvent : IDomainEvent
    {
        public BrowserChannel Channel { get; }

        public ChannelRemovedDomainEvent(BrowserChannel channel) 
        { 
            Channel = channel; 
        }
    }
}
