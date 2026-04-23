using System.Diagnostics.CodeAnalysis;
using Playhouse.Domain.SharedKernel.Resources.Strings;
using Playhouse.Domain.SharedKernel.Specifications.Validation;

namespace Playhouse.Domain.SharedKernel.Exceptions
{
    public sealed class ValidationSpecificationIsNullDomainException : DomainException
    {
        public ValidationSpecificationIsNullDomainException()
        {
        }

        public ValidationSpecificationIsNullDomainException(string? message) : base(message)
        {
        }

        public ValidationSpecificationIsNullDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfNull<TEntity>([NotNull] IValidationSpecification<TEntity> specification)
        {
            if (specification == null)
            {
                Throw();
            }
        }

        [DoesNotReturn]
        private static void Throw()
        {
            throw new ValidationSpecificationIsNullDomainException(DomainExceptionMessages.ValidationSpecificationisNull);
        }
    }
}
