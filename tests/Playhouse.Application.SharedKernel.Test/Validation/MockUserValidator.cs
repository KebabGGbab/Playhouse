using FluentValidation;
using Playhouse.Application.SharedKernel.Test.Mock;
using Playhouse.Application.SharedKernel.Validators.PropertyValidators;

namespace Playhouse.Application.SharedKernel.Test.Validation
{
    internal sealed class MockUserValidator : AbstractValidator<MockUser>
    {
        public MockUserValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(u => u.Name).ValidValueObject(MockValueObject.Create!);

            RuleFor(u => u.Age).CollectionContains([1,3,4]);
        }
    }
}
