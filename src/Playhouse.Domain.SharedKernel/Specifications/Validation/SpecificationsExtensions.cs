namespace Playhouse.Domain.SharedKernel.Specifications.Validation
{
    public static class SpecificationsExtensions
    {
        /// <summary>
        /// Валидирует оба правила.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IValidationSpecification<TEntity> And<TEntity>(this IValidationSpecification<TEntity> left, IValidationSpecification<TEntity> right)
        {
            return new AndValidationSpecification<TEntity>(left, right);
        }

        /// <summary>
        /// Валидирует второе правило только в том случае, если первое прошло успешно.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IValidationSpecification<TEntity> Then<TEntity>(this IValidationSpecification<TEntity> left, IValidationSpecification<TEntity> right)
        {
            return new ThenValidationSpecification<TEntity>(left, right);
        }
    }
}
