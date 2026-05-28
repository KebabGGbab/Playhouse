using Ardalis.SmartEnum;
using FluentValidation;
using FluentValidation.Results;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Application.Validators.PropertyValidators
{
    public static class PropertyValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, TElement> CollectionContains<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, ICollection<TElement> collection)
        {
            return ruleBuilder.SetValidator(new CollectionContainsValidator<T, TElement>(collection));
        }

        public static IRuleBuilderOptions<T, string?> IsSmartEnum<T, TSmartEnum, TEnumValue>(this IRuleBuilder<T, string?> ruleBuilder)
            where TSmartEnum : SmartEnum<TSmartEnum, TEnumValue>
            where TEnumValue : IEquatable<TEnumValue>, IComparable<TEnumValue>
        {
            return ruleBuilder.SetValidator(new IsSmartEnumValidator<T, TSmartEnum, TEnumValue>());
        }

        public static IRuleBuilderOptionsConditions<T, TValue> ValidValueObject<T, TValue>(this IRuleBuilder<T, TValue> ruleBuilder, Func<TValue, Result> validator)
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result result = validator(value);

                if (result.IsSuccess)
                {
                    return;
                }

                foreach (Error error in result.Errors)
                {
                    context.AddFailure(new ValidationFailure(context.PropertyPath, error.Message, value));
                }
            });
        }
    }
}
