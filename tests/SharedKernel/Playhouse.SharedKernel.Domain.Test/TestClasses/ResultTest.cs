using Playhouse.SharedKernel.Domain.Exceptions;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Test.Mocks;

namespace Playhouse.SharedKernel.Domain.Test.TestClasses
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
            IEnumerable<Error> errors = [new MockNameError("error"), new MockNameError("error2")];

            Result result = Result.Fail(errors);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.HasCount(2, result.Errors);
        }


        [TestMethod]
        public void Fail_ErrorCollectionIsNull_Throw()
        {
            IEnumerable<Error> errors = null!;

            void action() => Result.Fail(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void Fail_ErrorCollectionIsEmpty_Throw()
        {
            IEnumerable<Error> errors = [];

            void action() => Result.Fail(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void Fail_ErrorCollectionContainsNull_Throw()
        {
            IEnumerable<Error> errors = [new MockNameError("error"), null!];

            void action() => Result.Fail(errors);

            Assert.ThrowsExactly<FailureResultContainsNullErrorDomainException>(action);
        }

        [TestMethod]
        public void Fail_OneError_ErrorAddInCollection()
        {
            MockNameError error = new("error");

            Result result = Result.Fail(error);

            Assert.ContainsSingle(result.Errors);
            Assert.Contains(error, result.Errors);
        }

        [TestMethod]
        public void Fail_OneErrorIsNull_Throw()
        {
            MockNameError error = null!;

            void action() => Result.Fail(error);

            Assert.ThrowsExactly<FailureResultContainsNullErrorDomainException>(action);
        }

        [TestMethod]
        public void Fail_TryAddNewErrorInSourceCollection_ErrorsCollectionIsImmutable()
        {
            MockNameError firstError = new("error1");
            List<Error> errors = [firstError];
            Result result = Result.Fail(errors);

            errors.Add(new MockNameError("error2"));

            Assert.ContainsSingle(result.Errors);
            Assert.Contains(firstError, result.Errors);
        }

        [TestMethod]
        public void OkOfT_Simple_IsSuccessAndErrorsEmptyAndHasValue()
        {
            int value = 111;

            Result<int> result = Result.Ok(value);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Errors);
            Assert.AreEqual(value, result.Value);
        }

        [TestMethod]
        public void FailOfT_ErrorCollectionIsNull_Throw()
        {
            IEnumerable<Error> errors = null!;

            void action() => Result.Fail<int>(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void FailOfT_ErrorCollectionIsEmpty_Throw()
        {
            IEnumerable<Error> errors = [];

            void action() => Result.Fail<object>(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void FailOfT_ErrorCollectionContainsNull_Throw()
        {
            IEnumerable<Error> errors = [new MockNameError("error"), null!];

            void action() => Result.Fail<int>(errors);

            Assert.ThrowsExactly<FailureResultContainsNullErrorDomainException>(action);
        }

        [TestMethod]
        public void FailOfT_OneError_ErrorAddInCollection()
        {
            MockNameError error = new("error");

            Result result = Result.Fail<int>(error);

            Assert.ContainsSingle(result.Errors);
            Assert.Contains(error, result.Errors);
        }

        [TestMethod]
        public void FailOfT_OneErrorIsNull_Throw()
        {
            MockNameError error = null!;

            void action() => Result.Fail<int>(error);

            Assert.ThrowsExactly<FailureResultContainsNullErrorDomainException>(action);
        }

        [TestMethod]
        public void GetValue_FailResult_Throw()
        {
            IEnumerable<Error> errors = [new MockNameError("error")];
            Result<int> result = Result.Fail<int>(errors);

            void action() => _ = result.Value;

            Assert.ThrowsExactly<TryGetValueFromFailureResultDomainException>(action);
        }
    }
}
