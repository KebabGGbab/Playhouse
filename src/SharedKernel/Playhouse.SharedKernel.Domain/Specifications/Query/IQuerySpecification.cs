namespace Playhouse.SharedKernel.Domain.Specifications.Query
{
    public interface IQuerySpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity candidate);
    }
}
