using Playhouse.Domain.SharedKernel.Exceptions;
using Playhouse.Domain.SharedKernel.Results;

namespace Playhouse.Domain.SharedKernel.Specifications.Validation
{
    internal sealed class AndValidationSpecification<TEntity> : IValidationSpecification<TEntity>
    {
        private readonly IValidationSpecification<TEntity> _left;
        private readonly IValidationSpecification<TEntity> _right;

        public AndValidationSpecification(IValidationSpecification<TEntity> left, IValidationSpecification<TEntity> right)
        {
            ValidationSpecificationIsNullDomainException.ThrowIfNull(left);
            ValidationSpecificationIsNullDomainException.ThrowIfNull(right);

            _left = left;
            _right = right;
        }

        public Result IsSatisfiedBy(TEntity entity)
        {
            Result resultLeft = _left.IsSatisfiedBy(entity);
            Result resultRight = _right.IsSatisfiedBy(entity);

            if (resultLeft.IsSuccess && resultRight.IsSuccess)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail(resultLeft.Errors.Concat(resultRight.Errors));
            }
        }
    }
}
