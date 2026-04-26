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

            FailureResultNotContainErrorDomainException.ThrowIfErrorCollectionEmpty(errors);
        }

        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsNull_Throw()
        {
            IEnumerable<Error>? errors = null;

            void action() => FailureResultNotContainErrorDomainException.ThrowIfErrorCollectionEmpty(errors);

            Assert.ThrowsExactly<FailureResultNotContainErrorDomainException>(action);
        }

        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsEmpty_Throw()
        {
            IEnumerable<Error> errors = [];

            void action() => FailureResultNotContainErrorDomainException.ThrowIfErrorCollectionEmpty(errors);

            Assert.ThrowsExactly<FailureResultNotContainErrorDomainException>(action);
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
    }
}
