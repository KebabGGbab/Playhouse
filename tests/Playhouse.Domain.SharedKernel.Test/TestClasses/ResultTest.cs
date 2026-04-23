using Playhouse.Domain.SharedKernel.Results;
using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses
{
    [TestClass]
    public sealed class ResultTest
    {
        [TestMethod]
        public void Ok_Simple_IsSuccessAndErrorsEmpty()
        {
            Result result = Result.Ok();

            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsFailure);
            Assert.IsEmpty(result.Errors);
        }

        [TestMethod]
        public void Fail_Simple_IsFailureAndHasErrors()
        {
            IEnumerable<Error> errors = [new MockNameError("error")];

            Result result = Result.Fail(errors);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotEmpty(result.Errors);
        }


        [TestMethod]
        public void Fail_ErrorCollectionIsNull_Throw()
        {
            IEnumerable<Error> errors = null!;

            void action() => Result.Fail(errors);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Fail_ErrorCollectionIsEmpty_Throw()
        {
            IEnumerable<Error> errors = [];

            void action() => Result.Fail(errors);

            Assert.ThrowsExactly<ArgumentException>(action);
        }

        [TestMethod]
        public void OkOfT_Simple_IsSuccessAndErrorsEmptyAndHasValue()
        {
            int value = 111;

            Result<int> result = Result.Ok(value);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsFailure);
            Assert.IsEmpty(result.Errors);
            Assert.AreEqual(value, result.Value);
        }

        [TestMethod]
        public void GetValue_FailResult_Throw()
        {
            IEnumerable<Error> errors = [new MockNameError("error")];
            Result<int> result = Result.Fail<int>(errors);

            void action() => _ = result.Value;

            Assert.ThrowsExactly<InvalidOperationException>(action);
        }

        [TestMethod]
        public void ErrorsCollectionIsImmutable()
        {
            List<Error> errors = [new MockNameError("error1")];
            Result result = Result.Fail(errors);

            errors.Add(new MockNameError("error2"));

            Assert.HasCount(1, result.Errors);
        }
    }
}
