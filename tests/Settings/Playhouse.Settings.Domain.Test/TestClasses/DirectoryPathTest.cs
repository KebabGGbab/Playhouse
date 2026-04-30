using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class DirectoryPathTest
    {
        [TestMethod]
        public void Default_PathToDirectoryWithNamePlayhouseInAppDataCurrentUser()
        {
            string defaultPathToData = @"C:\Users\artem\AppData\Local\Playhouse";

            DirectoryPath path = DirectoryPath.Default;

            Assert.AreEqual(defaultPathToData, path.Path);
        }

        [TestMethod]
        public void CanCreate_ValidPath_Ok()
        {
            string path = @"C:\Documents";

            Result result = DirectoryPath.CanCreate(path);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow(@"Documents")]
        [DataRow(@"\Documents")]
        [DataRow(@":\Documents")]
        public void CanCreate_NotAbsolutePath_Fail(string path)
        {
            Result result = DirectoryPath.CanCreate(path);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void CanCreate_PathHasInvalidChars_Fail()
        {
            string path = @"C:\Documents|";

            Result result = DirectoryPath.CanCreate(path);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void CanCreate_PathIsNullOrWhiteSpace_Fail(string? path)
        {
            Result result = DirectoryPath.Create(path!);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_ValidPath_Ok()
        {
            string path = @"C:\Documents";

            Result<DirectoryPath> result = DirectoryPath.Create(path);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(path, result.Value.Path);
        }

        [TestMethod]
        public void Create_Transformation_Transformed()
        {
            string path = @" C:\Documents   ";
            string transormPath = @"C:\Documents";

            Result<DirectoryPath> result = DirectoryPath.Create(path);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(transormPath, result.Value.Path);
        }


        [TestMethod]
        [DataRow(@"Documents")]
        [DataRow(@"C:\Documents|")]
        [DataRow("")]
        public void Create_NotValidPath_Fail(string path)
        {
            Result<DirectoryPath> result = DirectoryPath.Create(path);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
