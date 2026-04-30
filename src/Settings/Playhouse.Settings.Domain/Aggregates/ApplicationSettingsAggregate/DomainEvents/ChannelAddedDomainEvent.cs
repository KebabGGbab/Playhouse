using Playhouse.SharedKernel.Domain.Events;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.DomainEvents
{
    public sealed class ChannelAddedDomainEvent : IDomainEvent
    {
        public BrowserChannel Channel { get; }

        public ChannelAddedDomainEvent(BrowserChannel channel)
        {
            Channel = channel;
        }
    }
}
