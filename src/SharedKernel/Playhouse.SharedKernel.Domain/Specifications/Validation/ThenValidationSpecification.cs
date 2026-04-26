using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Domain.Specifications.Validation
{
    internal sealed class ThenValidationSpecification<TEntity> : IValidationSpecification<TEntity>
    {
        private readonly IValidationSpecification<TEntity> _left;
        private readonly IValidationSpecification<TEntity> _right;

        public ThenValidationSpecification(IValidationSpecification<TEntity> left, IValidationSpecification<TEntity> right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);

            _left = left;
            _right = right;
        }

        public Result IsSatisfiedBy(TEntity entity)
        {
            Result resultLeft = _left.IsSatisfiedBy(entity);

            if (resultLeft.IsFailure)
            {
                return resultLeft;
            }

            return _right.IsSatisfiedBy(entity);
        }
    }
}
