using Playhouse.Domain.SharedKernel.Results;

namespace Playhouse.Domain.SharedKernel.Specifications.Validation
{
    public interface IValidationSpecification<TEntity>
    {
        Result IsSatisfiedBy(TEntity entity);
    }
}
