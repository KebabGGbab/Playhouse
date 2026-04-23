namespace Playhouse.Domain.SharedKernel.Events
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent;

        void AddEventHandler<TEvent>(IDomainEventHandler<TEvent> eventHandler)
            where TEvent : IDomainEvent;
    }
}
