using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using Playhouse.Domain.SharedKernel.SeedWork;

namespace Playhouse.Application.SharedKernel.Validators.PropertyValidators
{
    public sealed class ValueObjectValidator<T, TValue, TValueObject> : PropertyValidator<T, TValue>
    {
        private readonly Func<TValue, Result<TValueObject>> _factory;

        public ValueObjectValidator(Func<TValue, Result<TValueObject>> factory)
        {
            ArgumentNullException.ThrowIfNull(factory);

            _factory = factory;
        }

        public override string Name => nameof(ValueObjectValidator<,,>);

        public override bool IsValid(ValidationContext<T> context, TValue value)
        {
            Result<TValueObject> result = _factory(value);

            if (result.IsSuccess)
            {
                return true;
            }

            foreach (string error in result.Errors)
            {
                context.AddFailure(new ValidationFailure(context.PropertyPath, error, value));
            }

            return false;
        }
    }
}
