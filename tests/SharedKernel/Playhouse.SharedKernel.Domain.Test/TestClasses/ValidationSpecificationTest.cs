using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;
using Playhouse.SharedKernel.Domain.Test.Mocks;

namespace Playhouse.SharedKernel.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class ValidationSpecificationTest
    {
        private readonly MockLeghtNameValidationSpecification _lenghtNameRule;
        private readonly MockEmptyNameValidationSpecification _emptyNameRule;
        private readonly MockContainsOnlyLettersValidationSpecification _containsOnlyLettersRule;

        public ValidationSpecificationTest()
        {
            _lenghtNameRule = new();
            _emptyNameRule = new();
            _containsOnlyLettersRule = new();
        }

        [TestMethod]
        public void And_LeftRuleIsNull_Throw()
        {
            MockEmptyNameValidationSpecification leftRule = null!;

            void action() => leftRule.And(_lenghtNameRule);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void And_RightRuleIsNull_Throw()
        {
            MockEmptyNameValidationSpecification rightRule = null!;

            void action() => _emptyNameRule.And(rightRule);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void And_ValidArgument_Success()
        {
            string name = "artem";
            IValidationSpecification<string> validationRule = _containsOnlyLettersRule.And(_lenghtNameRule);

            Result result = validationRule.IsSatisfiedBy(name);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow("Steve1", 1)]
        [DataRow("Ю", 1)]
        [DataRow("1", 2)]
        public void And_RuleCombinations(string name, int countError)
        {
            IValidationSpecification<string> validationRule = _containsOnlyLettersRule.And(_lenghtNameRule);

            Result result = validationRule.IsSatisfiedBy(name);

            Assert.IsTrue(result.IsFailure);
            Assert.HasCount(countError, result.Errors);
        }

        [TestMethod]
        public void Then_LeftRuleIsNull_Throw()
        {
            MockEmptyNameValidationSpecification leftRule = null!;

            void action() => leftRule.Then(_lenghtNameRule);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Then_RightRuleIsNull_Throw()
        {
            MockEmptyNameValidationSpecification rightRule = null!;

            void action() => _emptyNameRule.Then(rightRule);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Then_ValidArgument_Success()
        {
            string name = "artem";
            IValidationSpecification<string> validationRule = _emptyNameRule.Then(_lenghtNameRule);

            Result result = validationRule.IsSatisfiedBy(name);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Ю")]
        [DataRow("")]
        public void Then_NotValidArgument_OneError(string name)
        {
            IValidationSpecification<string> validationRule = _emptyNameRule.Then(_lenghtNameRule);

            Result result = validationRule.IsSatisfiedBy(name);

            Assert.IsTrue(result.IsFailure);
            Assert.ContainsSingle(result.Errors);
        }
    }
}
