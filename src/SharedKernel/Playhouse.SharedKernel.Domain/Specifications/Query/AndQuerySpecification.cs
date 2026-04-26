namespace Playhouse.SharedKernel.Domain.Specifications.Query
{
    internal sealed class AndQuerySpecification<TEntity> : IQuerySpecification<TEntity>
    {
        private readonly IQuerySpecification<TEntity> _left;
        private readonly IQuerySpecification<TEntity> _right;

        public AndQuerySpecification(IQuerySpecification<TEntity> left, IQuerySpecification<TEntity> right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);

            _left = left;
            _right = right;
        }


        public bool IsSatisfiedBy(TEntity candidate)
        {
            return _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
        }
    }
}
