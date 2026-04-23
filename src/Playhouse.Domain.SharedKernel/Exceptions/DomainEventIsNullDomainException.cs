using System.Diagnostics.CodeAnalysis;
using Playhouse.Domain.SharedKernel.Events;
using Playhouse.Domain.SharedKernel.Resources.Strings;

namespace Playhouse.Domain.SharedKernel.Exceptions
{
    public class DomainEventIsNullDomainException : DomainException
    {
        public DomainEventIsNullDomainException()
        {
        }

        public DomainEventIsNullDomainException(string? message) : base(message)
        {
        }

        public DomainEventIsNullDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfNull([NotNull] IDomainEvent? domainEvent)
        {
            if (domainEvent == null)
            {
                Throw();
            }
        }

        [DoesNotReturn]
        private static void Throw()
        {
            throw new DomainEventIsNullDomainException(DomainExceptionMessages.DomainEventIsNull);
        }
    }
}
