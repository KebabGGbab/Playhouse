using FluentValidation;
using Playhouse.Domain.SharedKernel.SeedWork;

namespace Playhouse.Application.SharedKernel.Validators.PropertyValidators
{
    public static class PropertyValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, TElement> CollectionContains<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, ICollection<TElement> collection)
        {
            return ruleBuilder.SetValidator(new CollectionContainsValidator<T, TElement>(collection));
        }

        public static IRuleBuilderOptions<T, TValue> ValidValueObject<T, TValue, TValueObject>(this IRuleBuilder<T, TValue> ruleBuilder, Func<TValue, Result<TValueObject>> factory)
        {
            return ruleBuilder.SetValidator(new ValueObjectValidator<T, TValue, TValueObject>(factory));
        }
    }
}
