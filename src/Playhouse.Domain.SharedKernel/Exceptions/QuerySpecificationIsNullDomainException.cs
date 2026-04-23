using System.Diagnostics.CodeAnalysis;
using Playhouse.Domain.SharedKernel.Resources.Strings;
using Playhouse.Domain.SharedKernel.Specifications.Query;

namespace Playhouse.Domain.SharedKernel.Exceptions
{
    public sealed class QuerySpecificationIsNullDomainException : DomainException
    {
        public QuerySpecificationIsNullDomainException()
        {
        }

        public QuerySpecificationIsNullDomainException(string? message) : base(message)
        {
        }

        public QuerySpecificationIsNullDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfNull<TEntity>([NotNull] IQuerySpecification<TEntity> specification)
        {
            if (specification == null)
            {
                Throw();
            }
        }

        [DoesNotReturn]
        private static void Throw()
        {
            throw new QuerySpecificationIsNullDomainException(DomainExceptionMessages.QuerySpecificationisNull);
        }
    }
}
