using Playhouse.Domain.SharedKernel.Exceptions;
using Playhouse.Domain.SharedKernel.Results;

namespace Playhouse.Domain.SharedKernel.Specifications.Validation
{
    internal sealed class ThenValidationSpecification<TEntity> : IValidationSpecification<TEntity>
    {
        private readonly IValidationSpecification<TEntity> _left;
        private readonly IValidationSpecification<TEntity> _right;

        public ThenValidationSpecification(IValidationSpecification<TEntity> left, IValidationSpecification<TEntity> right)
        {
            ValidationSpecificationIsNullDomainException.ThrowIfNull(left);
            ValidationSpecificationIsNullDomainException.ThrowIfNull(right);

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
