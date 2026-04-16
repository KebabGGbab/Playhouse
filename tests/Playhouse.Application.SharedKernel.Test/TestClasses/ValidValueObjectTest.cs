using FluentValidation.TestHelper;
using Playhouse.Application.SharedKernel.Test.Mock;
using Playhouse.Application.SharedKernel.Test.Validation;

namespace Playhouse.Application.SharedKernel.Test.TestClasses
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
            MockUser mockUser = new()
            {
                Name = "Steve",
                Age = 1
            };

            TestValidationResult<MockUser> result = _validator.TestValidate(mockUser);

            result.ShouldNotHaveValidationErrorFor(u => u.Name);
        }


        [TestMethod]
        [DataRow(null, "Name is required.")]
        [DataRow("", "Name is required.")]
        [DataRow(" ", "Name is required.")]
        [DataRow("A", "The name must be at least 3 characters long.")]
        public void ValidateValueObjectWithNotValidArgument(string name, string error)
        {
            MockUser mockUser = new()
            {
                Name = name,
                Age = 1
            };

            TestValidationResult<MockUser> result = _validator.TestValidate(mockUser);

            result.ShouldHaveValidationErrorFor(u => u.Name)
                .WithErrorMessage(error)
                .Only();
        }
    }
}
