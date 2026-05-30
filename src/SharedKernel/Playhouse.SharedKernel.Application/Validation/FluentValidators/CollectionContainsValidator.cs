using FluentValidation;
using FluentValidation.Validators;
using Playhouse.SharedKernel.Application.Resources.Strings;

namespace Playhouse.SharedKernel.Application.Validation.FluentValidators
{
    public sealed class CollectionContainsValidator<T, TCollectionElement> : PropertyValidator<T, TCollectionElement>
    {
        private readonly ICollection<TCollectionElement> _collection;

        public override string Name => nameof(CollectionContainsValidator<,>);

        public CollectionContainsValidator(ICollection<TCollectionElement> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            _collection = collection;
        }

        public override bool IsValid(ValidationContext<T> context, TCollectionElement value)
        {
            // If the value is null then we abort and assume success.
            // This should not be a failure condition - only a NotNull/NotEmpty should cause a null to fail.
            if (value == null)
            {
                return true;
            }

            if (!_collection.Contains(value))
            {
                context.MessageFormatter
                    .AppendArgument("Element", value);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return ValidationMessageTemplate.CollectionContainsValidator;
        }
    }
}
