using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Domain.Specifications.Validation
{
    public interface IValidationSpecification<TEntity>
    {
        Result IsSatisfiedBy(TEntity entity);
    }
}
