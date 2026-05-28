using FluentValidation.TestHelper;
using Playhouse.SharedKernel.Application.Test.Mock;
using Playhouse.SharedKernel.Application.Test.Validation;

namespace Playhouse.SharedKernel.Application.Test.TestClasses
{
    [TestClass]
    public sealed class ValidValueObjectTest
    {
        private readonly MockUserValidator _validator;

        public ValidValueObjectTest()
        {
            _validator = new MockUserValidator();
        }

        [TestMethod]
        public void ValidateValueObjectWithValidArgument()
        {
            MockUser mock = new() { Name = "Steve" };

            TestValidationResult<MockUser> result = _validator.TestValidate(mock);

            result.ShouldNotHaveValidationErrorFor(u => u.Name);
        }

        [TestMethod]
        [DataRow(null, "Name is required.")]
        [DataRow("", "Name is required.")]
        [DataRow(" ", "Name is required.")]
        [DataRow("A", "The name must be at least 3 characters long.")]
        public void ValidateValueObjectWithNotValidArgument(string name, string error)
        {
            MockUser mock = new() { Name = name };

            TestValidationResult<MockUser> result = _validator.TestValidate(mock);

            result.ShouldHaveValidationErrorFor(u => u.Name)
                .WithErrorMessage(error)
                .Only();
        }
    }
}
