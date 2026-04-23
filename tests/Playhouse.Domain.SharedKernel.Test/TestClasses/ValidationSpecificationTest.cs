using Playhouse.Domain.SharedKernel.Exceptions;
using Playhouse.Domain.SharedKernel.Results;
using Playhouse.Domain.SharedKernel.Specifications.Validation;
using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses
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

            Assert.ThrowsExactly<ValidationSpecificationIsNullDomainException>(action);
        }

        [TestMethod]
        public void And_RightRuleIsNull_Throw()
        {
            MockEmptyNameValidationSpecification rightRule = null!;

            void action() => _emptyNameRule.And(rightRule);

            Assert.ThrowsExactly<ValidationSpecificationIsNullDomainException>(action);
        }

        [TestMethod]
        [DataRow("artem", 0)]
        [DataRow("Steve1", 1)]
        [DataRow("Ю", 1)]
        [DataRow("1", 2)]
        public void And_RuleCombinations(string name, int countError)
        {
            IValidationSpecification<string> validationRule = _containsOnlyLettersRule.And(_lenghtNameRule);

            Result result = validationRule.IsSatisfiedBy(name);

            Assert.HasCount(countError, result.Errors);
        }

        [TestMethod]
        public void Then_LeftRuleIsNull_Throw()
        {
            MockEmptyNameValidationSpecification leftRule = null!;

            void action() => leftRule.Then(_lenghtNameRule);

            Assert.ThrowsExactly<ValidationSpecificationIsNullDomainException>(action);
        }

        [TestMethod]
        public void Then_RightRuleIsNull_Throw()
        {
            MockEmptyNameValidationSpecification rightRule = null!;

            void action() => _emptyNameRule.Then(rightRule);

            Assert.ThrowsExactly<ValidationSpecificationIsNullDomainException>(action);
        }

        [TestMethod]
        [DataRow("artem", 0)]
        [DataRow(null, 1)]
        [DataRow("Ю", 1)]
        [DataRow("", 1)]
        public void Then_RuleCombinations(string name, int countError)
        {
            IValidationSpecification<string> validationRule = _emptyNameRule.Then(_lenghtNameRule);

            Result result = validationRule.IsSatisfiedBy(name);

            Assert.HasCount(countError, result.Errors);
        }
    }
}
