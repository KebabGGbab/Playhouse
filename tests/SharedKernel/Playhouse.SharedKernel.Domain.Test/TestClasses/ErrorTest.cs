using Playhouse.SharedKernel.Domain.Test.Mocks;

namespace Playhouse.SharedKernel.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class ErrorTest 
    {
        [TestMethod]
        public void Ctor_MessageAndCodeNotEmpty_ObjectIsInitialized()
        {
            string message = "error";

            MockNameError error = new(message);

            Assert.AreEqual(message, error.Message);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Ctor_MessageIsEmpty_Throw(string? errorMessage)
        {
            void action() => _ = new MockNameError(errorMessage!);

           Assert.Throws<ArgumentException>(action);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Ctor_CodeIsEmpty_Throw(string? errorCode)
        {
            string message = "error";

            void action() => _ = new MockNameError(message, errorCode!);

            Assert.Throws<ArgumentException>(action);
        }
    }
}
