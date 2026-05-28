using FluentValidation.TestHelper;
using Playhouse.SharedKernel.Application.Test.Mock;
using Playhouse.SharedKernel.Application.Test.Validation;

namespace Playhouse.SharedKernel.Application.Test.TestClasses
{
    [TestClass]
    public sealed class IsSmartEnumValidatorTest
    {
        private readonly MockUserValidator _validator;

        public IsSmartEnumValidatorTest()
        {
            _validator = new MockUserValidator();
        }

        [TestMethod]
        public void Validate_StringIsSmartEnumName_IsValid()
        {
            MockUser mock = new() { DayOfBirthday = "Monday" };

            TestValidationResult<MockUser> result = _validator.TestValidate(mock);

            result.ShouldNotHaveValidationErrorFor(u => u.DayOfBirthday);
        }

        [TestMethod]
        public void Validate_StringIsNotSmartEnumName_IsNotValid()
        {
            MockUser mock = new() { DayOfBirthday = "Monday1" };

            TestValidationResult<MockUser> result = _validator.TestValidate(mock);

            result.ShouldHaveValidationErrorFor(u => u.DayOfBirthday)
                .WithErrorMessage("'Monday1' must be 'MockDayWeek'.");
        }
    }
}
