using System.Diagnostics.CodeAnalysis;
using Playhouse.SharedKernel.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Domain.Exceptions
{
    public sealed class FailureResultIsEmptyDomainException : DomainException
    {
        public FailureResultIsEmptyDomainException()
        {
        }

        public FailureResultIsEmptyDomainException(string? message) : base(message)
        {
        }

        public FailureResultIsEmptyDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfErrorCollectionEmpty([NotNull] IEnumerable<Error>? errors)
        {
            if (errors == null || errors.Any() == false)
            {
                Throw();
            }
        }

        [DoesNotReturn]
        private static void Throw()
        {
            throw new FailureResultIsEmptyDomainException(DomainExceptionMessages.FailureResultIsEmpty);
        }
    }
}
