namespace Playhouse.SharedKernel.Domain.Specifications.Query
{
    public static class SpecificationExtensions
    {
        public static IQuerySpecification<TEntity> And<TEntity>(this IQuerySpecification<TEntity> left, IQuerySpecification<TEntity> right)
        {
            return new AndQuerySpecification<TEntity>(left, right);
        }

        public static IQuerySpecification<TEntity> Or<TEntity>(this IQuerySpecification<TEntity> left, IQuerySpecification<TEntity> right)
        {
            return new OrQuerySpecification<TEntity>(left, right);
        }

        public static IQuerySpecification<TEntity> Not<TEntity>(this IQuerySpecification<TEntity> left)
        {
            return new NotQuerySpecification<TEntity>(left);
        }
    }
}
