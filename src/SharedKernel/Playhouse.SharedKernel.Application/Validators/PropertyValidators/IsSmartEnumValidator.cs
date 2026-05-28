using Ardalis.SmartEnum;
using FluentValidation;
using FluentValidation.Validators;
using Playhouse.SharedKernel.Application.Resources.Strings;

namespace Playhouse.SharedKernel.Application.Validators.PropertyValidators
{
    public sealed class IsSmartEnumValidator<T, TSmartEnum, TEnumValue> : PropertyValidator<T, string?>
        where TSmartEnum : SmartEnum<TSmartEnum, TEnumValue>
        where TEnumValue : IEquatable<TEnumValue>, IComparable<TEnumValue>
    {
        public override string Name => nameof(IsSmartEnumValidator<,,>);

        public override bool IsValid(ValidationContext<T> context, string? value)
        {
            // If the value is null then we abort and assume success.
            // This should not be a failure condition - only a NotNull/NotEmpty should cause a null to fail.
            if (value == null)
            {
                return true;
            }

            if (!SmartEnum<TSmartEnum, TEnumValue>.TryFromName(value, out _))
            {
                context.MessageFormatter
                    .AppendArgument("SmartEnum", typeof(TSmartEnum).Name);

                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return ValidationMessageTemplate.IsSmartEnumValidator;
        }
    }
}
