using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses
{
    [TestClass]
    public sealed class ErrorTest
    {
        [TestMethod]
        public void Ctor_Simple_ObjectIsInitialized()
        {
            string message = "error";

            MockNameError error = new(message);

            Assert.AreEqual(message, error.Message);
        }

        [TestMethod]
        public void Ctor_MessageIsNull_Throw()
        {
            string message = null!;

            void action() => _ = new MockNameError(message);

           Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Ctor_MessageIsEmpty_Throw()
        {
            string message = string.Empty;

            void action() => _ = new MockNameError(message);

            Assert.ThrowsExactly<ArgumentException>(action);
        }

        [TestMethod]
        public void Ctor_MessageIsWhiteSpace_Throw()
        {
            string message = " ";

            void action() => _ = new MockNameError(message);

            Assert.ThrowsExactly<ArgumentException>(action);
        }
    }
}
