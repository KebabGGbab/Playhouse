using Playhouse.SharedKernel.Domain.Exceptions;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Test.Mocks;

namespace Playhouse.SharedKernel.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class DomainExceptionsTest
    {
        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsNotEmpty_NotThrow()
        {
            IEnumerable<Error> errors = [new MockNameError("error")];

            FailureResultIsEmptyDomainException.ThrowIfErrorCollectionEmpty(errors);
        }

        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsNull_Throw()
        {
            IEnumerable<Error>? errors = null;

            void action() => FailureResultIsEmptyDomainException.ThrowIfErrorCollectionEmpty(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsEmpty_Throw()
        {
            IEnumerable<Error> errors = [];

            void action() => FailureResultIsEmptyDomainException.ThrowIfErrorCollectionEmpty(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void TryGetValueFromFailureResultDomainException_ThrowIfFailure_IsSuccess_NotThrow()
        {
            bool isFailure = false;

            TryGetValueFromFailureResultDomainException.ThrowIfFailure(isFailure);
        }

        [TestMethod]
        public void TryGetValueFromFailureResultDomainException_ThrowIfFailure_IsFailure_Throw()
        {
            bool isFailure = true;

            void action() => TryGetValueFromFailureResultDomainException.ThrowIfFailure(isFailure);

            Assert.ThrowsExactly<TryGetValueFromFailureResultDomainException>(action);
        }

        [TestMethod]
        public void FailureResultContainsNullErrorDomainException_ThrowIfContainsNull_NotContainsNull_NotThrow()
        {
            IEnumerable<Error> errors = [new MockNameError("error"), new MockNameError("error2")];

            FailureResultContainsNullErrorDomainException.ThrowIfContainsNull(errors);
        }

        [TestMethod]
        public void FailureResultContainsNullErrorDomainException_ThrowIfContainsNull_ContainsNull_Throw()
        {
            IEnumerable<Error> errors = [new MockNameError("error"), null!];

            void action() => FailureResultContainsNullErrorDomainException.ThrowIfContainsNull(errors);

            Assert.ThrowsExactly<FailureResultContainsNullErrorDomainException>(action);
        }
    }
}
