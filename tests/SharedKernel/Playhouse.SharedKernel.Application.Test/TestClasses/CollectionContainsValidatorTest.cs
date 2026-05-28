using FluentValidation.TestHelper;
using Playhouse.SharedKernel.Application.Test.Mock;
using Playhouse.SharedKernel.Application.Test.Validation;

namespace Playhouse.SharedKernel.Application.Test.TestClasses
{
    [TestClass]
    public sealed class CollectionContainsValidatorTest
    {
        private readonly MockUserValidator _validator;

        public CollectionContainsValidatorTest()
        {
            _validator = new MockUserValidator();
        }

        [TestMethod]
        public void ValidateWithContainsElement()
        {
            MockUser mock = new() { Age = 1 };

            TestValidationResult<MockUser> result = _validator.TestValidate(mock);

            result.ShouldNotHaveValidationErrorFor(u => u.Age);
        }

        [TestMethod]
        public void ValidateWithNotContainsElement()
        {
            MockUser mock = new() { Age = 2 }; ;

            TestValidationResult<MockUser> result = _validator.TestValidate(mock);

            result.ShouldHaveValidationErrorFor(u => u.Age)
                .WithErrorMessage("The collection does not contain the element '2'.");
        }
    }
}
