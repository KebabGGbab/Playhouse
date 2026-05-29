using System.Diagnostics.CodeAnalysis;
using Playhouse.SharedKernel.Domain.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Domain.Exceptions
{
    public sealed class FailureResultContainsNullErrorDomainException : DomainException
    {
        public FailureResultContainsNullErrorDomainException()
        {
        }

        public FailureResultContainsNullErrorDomainException(string? message) : base(message)
        {
        }

        public FailureResultContainsNullErrorDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfContainsNull(IEnumerable<Error> errors)
        {
            if (errors.Any(e => e == null))
            {
                Throw();
            }
        }

        [DoesNotReturn]
        private static void Throw()
        {
            throw new FailureResultContainsNullErrorDomainException(DomainExceptionMessages.FailureResultContainsNullError);
        }
    }
}
