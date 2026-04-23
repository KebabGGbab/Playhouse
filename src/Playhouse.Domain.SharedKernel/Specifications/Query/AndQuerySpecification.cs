using Playhouse.Domain.SharedKernel.Exceptions;

namespace Playhouse.Domain.SharedKernel.Specifications.Query
{
    internal sealed class AndQuerySpecification<TEntity> : IQuerySpecification<TEntity>
    {
        private readonly IQuerySpecification<TEntity> _left;
        private readonly IQuerySpecification<TEntity> _right;

        public AndQuerySpecification(IQuerySpecification<TEntity> left, IQuerySpecification<TEntity> right)
        {
            QuerySpecificationIsNullDomainException.ThrowIfNull(left);
            QuerySpecificationIsNullDomainException.ThrowIfNull(right);

            _left = left;
            _right = right;
        }


        public bool IsSatisfiedBy(TEntity candidate)
        {
            return _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
        }
    }
}
