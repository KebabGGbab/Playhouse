using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Domain.Specifications.Validation
{
    internal sealed class AndValidationSpecification<TEntity> : IValidationSpecification<TEntity>
    {
        private readonly IValidationSpecification<TEntity> _left;
        private readonly IValidationSpecification<TEntity> _right;

        public AndValidationSpecification(IValidationSpecification<TEntity> left, IValidationSpecification<TEntity> right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);

            _left = left;
            _right = right;
        }

        public Result IsSatisfiedBy(TEntity entity)
        {
            Result resultLeft = _left.IsSatisfiedBy(entity);
            Result resultRight = _right.IsSatisfiedBy(entity);

            if (resultLeft.IsFailure && resultRight.IsFailure)
            {
                return Result.Fail(resultLeft.Errors.Concat(resultRight.Errors));
            }
            else if (resultLeft.IsFailure)
            {
                return resultLeft;
            }
            else if (resultRight.IsFailure)
            {
                return resultRight;
            }

            return Result.Ok();
        }
    }
}
