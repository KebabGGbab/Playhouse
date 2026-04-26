using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Domain.Test.Mocks
{
    internal sealed class MockNameError : Error
    {
        public MockNameError(string message, string code = "MockNameError.Length") : base(message, code)
        {
        }
    }
}
