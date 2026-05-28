using Playhouse.SharedKernel.Domain.Events;

namespace Playhouse.SharedKernel.Infrastructure.EventBus
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly HashSet<object> _handlers;

        public DomainEventDispatcher()
        {
            _handlers = [];
        }

        public void AddEventHandler<TEvent>(IDomainEventHandler<TEvent> eventHandler) 
            where TEvent : IDomainEvent
        {
            ArgumentNullException.ThrowIfNull(eventHandler);

            _handlers.Add(eventHandler);
        }

        public void Dispatch<TEvent>(TEvent domainEvent) 
            where TEvent : IDomainEvent
        {
            ArgumentNullException.ThrowIfNull(domainEvent);

            foreach (IDomainEventHandler<TEvent> handler in _handlers.OfType<IDomainEventHandler<TEvent>>())
            {
                handler.Handle(domainEvent);
            }
        }
    }
}
