using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class CultureTest
    {
        [TestMethod]
        public void Default()
        {
            string defaultCode = "en";

            Culture culture = Culture.Default;

            Assert.AreEqual(defaultCode, culture.Code);
        }

        [TestMethod]
        public void CanCreate_ValidCode_Ok()
        {
            string code = "ru";

            Result result = Culture.CanCreate(code);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CanCreate_NotSupportedCulture_Fail()
        {
            string code = "fr";

            Result result = Culture.CanCreate(code);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void CanCreate_CodeIsEmpty_Fail(string? cultureName)
        {
            Result<Culture> result = Culture.Create(cultureName!);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_ValidCode_Ok()
        {
            string code = "ru";

            Result<Culture> result = Culture.Create(code);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(code, result.Value.Code);
        }

        [TestMethod]
        public void Create_Transformation_Transformed()
        {
            string code = "Ru ";
            string transformCode = "ru";

            Result<Culture> result = Culture.Create(code);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(transformCode, result.Value.Code);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("de")]
        public void Create_NotValidCode_Fail(string? cultureName)
        {
            Result<Culture> result = Culture.Create(cultureName!);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
