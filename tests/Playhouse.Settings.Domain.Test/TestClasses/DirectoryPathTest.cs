using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class DirectoryPathTest
    {
        [TestMethod]
        public void Default()
        {
            string defaultPathToData = @"C:\Users\artem\AppData\Local\Playhouse.Settings.Domain";

            DirectoryPath path = DirectoryPath.Default;

            Assert.AreEqual(defaultPathToData, path.Path);
        }

        [TestMethod]
        public void Create_Simple_ResultOk()
        {
            string path = @"C:\Documents";

            Result<DirectoryPath> result = DirectoryPath.Create(path);

            Assert.AreEqual(path, result.Value!.Path);
        }

        [TestMethod]
        [DataRow(@"Documents")]
        [DataRow(@"\Documents")]
        [DataRow(@":\Documents")]
        public void Create_NotAbsolutePath_ResultFail(string path)
        {
            Result<DirectoryPath> result = DirectoryPath.Create(path);

            Assert.Contains("Путь к каталогу должен быть абсолютным.", result.Errors!);
        }

        [TestMethod]
        public void Create_PathWithInvalidChars_ResultFail()
        {
            string path = @"C:\Documents|";

            Result<DirectoryPath> result = DirectoryPath.Create(path);

            Assert.Contains("Путь к каталогу содержит недопустимые символы.", result.Errors!);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void Create_PathIsNullOrWhiteSpace_ResultFail(string? path)
        {
            Result<DirectoryPath> result = DirectoryPath.Create(path!);

            Assert.HasCount(1, result.Errors!);
            Assert.Contains("Путь к каталогу не указан.", result.Errors!);
        }
    }
}
