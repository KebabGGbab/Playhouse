namespace Playhouse.SharedKernel.Domain.Specifications.Query
{
    internal sealed class NotQuerySpecification<TEntity> : IQuerySpecification<TEntity>
    {
        private readonly IQuerySpecification<TEntity> _wrapped;

        public NotQuerySpecification(IQuerySpecification<TEntity> wrapped)
        {
            ArgumentNullException.ThrowIfNull(wrapped);

            _wrapped = wrapped;
        }

        public bool IsSatisfiedBy(TEntity candidate)
        {
            return !_wrapped.IsSatisfiedBy(candidate);
        }
    }
}
