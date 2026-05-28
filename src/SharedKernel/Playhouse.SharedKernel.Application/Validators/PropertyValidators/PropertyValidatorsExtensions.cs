using FluentValidation;
using FluentValidation.Results;
using Playhouse.SharedKernel.Domain.BaseModels;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Application.Validators.PropertyValidators
{
    public static class PropertyValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, TElement> CollectionContains<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, ICollection<TElement> collection)
        {
            return ruleBuilder.SetValidator(new CollectionContainsValidator<T, TElement>(collection));
        }

        public static IRuleBuilderOptionsConditions<T, TValue> ValidValueObject<T, TValue, TValueObject>(this IRuleBuilder<T, TValue> ruleBuilder, Func<TValue, Result<TValueObject>> factory)
            where TValueObject : ValueObject
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject> result = factory(value);

                if (result.IsSuccess)
                {
                    return;
                }

                foreach (string error in result.Errors)
                {
                    context.AddFailure(new ValidationFailure(context.PropertyPath, error, value));
                }
            });
        }
    }
}
