using System.Reflection;
using Playhouse.Domain.SharedKernel.SeedWork;

namespace Playhouse.Domain.SharedKernel
{
    public static class DomainEventsDispatcher
    {
        private static readonly List<Type> _handlers;

        static DomainEventsDispatcher()
        {
            _handlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandlerDomainEvent<>)))
                .ToList();
        }

        public static void Dispatch(IDomainEvent domainEvent)
        {
            foreach (Type handlerType in _handlers)
            {
                bool canHandleEvent = handlerType.GetInterfaces()
                    .Any(x => x.IsGenericType
                    && x.GetGenericTypeDefinition() == typeof(IHandlerDomainEvent<>)
                    && x.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandleEvent)
                {
                    dynamic? handler = Activator.CreateInstance(handlerType);
                    handler?.Handle((dynamic)domainEvent);
                }
            }
        }
    }
}
