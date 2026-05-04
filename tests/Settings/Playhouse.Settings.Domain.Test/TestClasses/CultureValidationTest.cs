using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.Culture;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class CultureValidationTest
    {

        // Методы называются по шаблону:
        // "Имя валидируемого свойства"_"Правило валидации"_"Сценарий"_"Результат"

        [TestMethod]
        public void Code_NotEmpty_IsNotEmpty_Ok()
        {
            string code = "jn";
            CultureCodeNotEmptyValidationSpecification rule = new();

            Result result = rule.IsSatisfiedBy(code);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void Code_NotEmpty_CultureIsEmpty_Fail(string? code)
        {
            CultureCodeNotEmptyValidationSpecification rule = new();

            Result result = rule.IsSatisfiedBy(code!);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Code_NotSupported_Supported_Ok()
        {
            string code = "ru";
            CultureCodeNotSupportedValidationSpecification rule = new(["ru"]);

            Result result = rule.IsSatisfiedBy(code);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Code_NotSupported_NotSupported_Fail()
        {
            string code = "jn";
            CultureCodeNotSupportedValidationSpecification rule = new(["ru"]);

            Result result = rule.IsSatisfiedBy(code);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Code_Code_NotEmptyAndSupported_Ok()
        {
            string code = "ru";
            CodeValidationSpecification rule = new(["ru"]);

            Result result = rule.IsSatisfiedBy(code);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("jn")]
        public void Code_Code_EmptyOrNotSupported_Fail(string code)
        {
            CodeValidationSpecification rule = new(["ru"]);

            Result result = rule.IsSatisfiedBy(code);

            Assert.IsTrue(result.IsFailure);
            Assert.HasCount(1, result.Errors);
        }
    }
}
