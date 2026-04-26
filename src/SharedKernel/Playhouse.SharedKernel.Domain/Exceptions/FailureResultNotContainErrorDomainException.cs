using System.Diagnostics.CodeAnalysis;
using Playhouse.SharedKernel.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Domain.Exceptions
{
    public sealed class FailureResultNotContainErrorDomainException : DomainException
    {
        public FailureResultNotContainErrorDomainException()
        {
        }

        public FailureResultNotContainErrorDomainException(string? message) : base(message)
        {
        }

        public FailureResultNotContainErrorDomainException(string? message, Exception? innerException) : base(message, innerException)
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
            throw new FailureResultNotContainErrorDomainException(DomainExceptionMessages.FailureResultNotContainError);
        }
    }
}
