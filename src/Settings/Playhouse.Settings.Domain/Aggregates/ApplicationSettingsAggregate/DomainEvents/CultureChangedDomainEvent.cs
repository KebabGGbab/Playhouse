using Playhouse.SharedKernel.Domain.Events;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.DomainEvents
{
    public sealed class CultureChangedDomainEvent : IDomainEvent
    {
        public Culture OldCulture { get; }

        public Culture NewCulture { get; }

        public CultureChangedDomainEvent(Culture oldCulture, Culture newCulture)
        {
            OldCulture = oldCulture;
            NewCulture = newCulture;
        }
    }
}
