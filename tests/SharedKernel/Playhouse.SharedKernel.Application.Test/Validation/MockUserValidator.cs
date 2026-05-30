using FluentValidation;
using Playhouse.SharedKernel.Application.Test.Mock;
using Playhouse.SharedKernel.Application.Validation.FluentValidators;

namespace Playhouse.SharedKernel.Application.Test.Validation
{
    internal sealed class MockUserValidator : AbstractValidator<MockUser>
    {
        public MockUserValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(u => u.Name).ValidValueObject(MockValueObject.Create!);

            RuleFor(u => u.Age).CollectionContains([1,3,4]);

            RuleFor(u => u.DayOfBirthday).IsSmartEnum<MockUser, MockDayWeek, int>();
        }
    }
}
