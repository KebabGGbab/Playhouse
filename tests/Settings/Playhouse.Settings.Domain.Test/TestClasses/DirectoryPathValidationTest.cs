using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.DirectoryPath.Path;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Test.TestClasses
{

    // Методы называются по шаблону:
    // "Имя валидируемого свойства"_"Правило валидации"_"Сценарий"_"Результат"

    [TestClass]
    public sealed class DirectoryPathValidationTest
    {
        [TestMethod]
        public void Path_NotEmpty_NotEmpty_Ok()
        {
            NotEmptyValidationSpecification rule = new();
            string path = @"C:\Documents";

            Result result = rule.IsSatisfiedBy(path!);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void Path_NotEmpty_IsEmpty_Fail(string? path)
        {
            NotEmptyValidationSpecification rule = new();

            Result result = rule.IsSatisfiedBy(path!);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Path_Absolute_IsAbsolute_Ok()
        {
            AbsoluteValidationSpecification rule = new();
            string path = @"C:\Documents";

            Result result = rule.IsSatisfiedBy(path);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow(@"Documents")]
        [DataRow(@"\Documents")]
        [DataRow(@":\Documents")]
        public void Path_Absolute_NotAbsolute_Fail(string path)
        {
            AbsoluteValidationSpecification rule = new();

            Result result = rule.IsSatisfiedBy(path);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Path_HasNotInvalidChars_HasNotInvalidChars_Ok()
        {
            HasNotInvalidCharsValidationSpecifications rule = new();
            string path = @"C:\Documents";

            Result result = rule.IsSatisfiedBy(path);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Path_HasNotInvalidChars_HasInvalidChars_Fail()
        {
            HasNotInvalidCharsValidationSpecifications rule = new();
            string path = @"C:\Documents|";

            Result result = rule.IsSatisfiedBy(path);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Path_Path_AbsoluteAndHasNotInvalidChars_Ok()
        {
            PathValidationSpecification rule = new();
            string path = @"C:\Documents";

            Result result = rule.IsSatisfiedBy(path);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow(@"C:\Documents|")]
        [DataRow("C:Documents")]
        public void Path_Path_NotAbsoluteOrHasInvalidChars_Fail(string path)
        {
            PathValidationSpecification rule = new();

            Result result = rule.IsSatisfiedBy(path);

            Assert.IsTrue(result.IsFailure);
            Assert.ContainsSingle(result.Errors);
        }

        [TestMethod]
        public void Path_Path_NotAbsoluteAndHasInvalidChars_Fail()
        {
            PathValidationSpecification rule = new();
            string path = @"C:Documents|";

            Result result = rule.IsSatisfiedBy(path);

            Assert.IsTrue(result.IsFailure);
            Assert.HasCount(2, result.Errors);
        }
    }
}
