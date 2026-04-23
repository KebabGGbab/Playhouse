namespace Playhouse.Domain.SharedKernel.Specifications.Query
{
    public interface IQuerySpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity candidate);
    }
}
