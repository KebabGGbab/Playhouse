using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class CultureTest
    {
        [TestMethod]
        public void Default()
        {
            string defaultCultureName = "en";

            Culture culture = Culture.Default;

            Assert.AreEqual(defaultCultureName, culture.Name);
        }

        [TestMethod]
        public void Create_SupportedCulture_ResultOk()
        {
            string cultureName = "ru";

            Culture culture = Culture.Create(cultureName).Value!;

            Assert.AreEqual(cultureName, culture.Name);
        }

        [TestMethod]
        public void Create_SupportedCultureWithRegister_ResultOk()
        {
            string cultureName = "Ru";

            Culture culture = Culture.Create(cultureName).Value!;

            Assert.AreEqual(cultureName.ToLowerInvariant(), culture.Name);
        }

        [TestMethod]
        public void Create_NotSupportedCulture_ResultFail()
        {
            string cultureName = "fr";

            Result<Culture> result = Culture.Create(cultureName);

            Assert.Contains("Данная культура не поддерживается приложением.", result.Errors!);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void Create_CultureNameIsNullOrWhiteSpace_ResultFail(string? cultureName)
        {
            Result<Culture> result = Culture.Create(cultureName!);

            Assert.HasCount(1, result.Errors!);
            Assert.Contains("Культура не указана.", result.Errors!);
        }
    }
}
