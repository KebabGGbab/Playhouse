namespace Playhouse.Domain.SharedKernel.SeedWork
{
    internal interface IHandlerDomainEvent<TEvent>
        where TEvent : IDomainEvent
    {
        void Handle(TEvent domainEvent);
    }
}
