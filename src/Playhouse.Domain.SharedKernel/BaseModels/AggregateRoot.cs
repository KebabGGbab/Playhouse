using Playhouse.Domain.SharedKernel.Events;
using Playhouse.Domain.SharedKernel.Exceptions;

namespace Playhouse.Domain.SharedKernel.BaseModels
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents;

        public IReadOnlyList<IDomainEvent> Events { get; }

        public AggregateRoot()
        {
            _domainEvents = [];
            Events = _domainEvents.AsReadOnly();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            DomainEventIsNullDomainException.ThrowIfNull(domainEvent);

            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
