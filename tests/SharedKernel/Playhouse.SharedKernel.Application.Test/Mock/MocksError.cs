using Playhouse.SharedKernel.Application.Test.Resources.Strings;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Application.Test.Mock
{
    internal sealed class MockNameIsNullError : Error
    {
        public MockNameIsNullError() : base(ErrorMessages.MockValueObjectNameIsNull, "MockValueObject.Name.IsNull")
        {
        }
    }

    internal sealed class MockNameIsShortError : Error
    {
        public MockNameIsShortError() : base(ErrorMessages.MockValueObjectNameIsShort, "MockValueObject.Name.IsShort")
        {
        }
    }
}
