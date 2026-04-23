using Playhouse.Domain.SharedKernel.Results;

namespace Playhouse.Domain.SharedKernel.Test.Mocks
{
    internal sealed class MockNameError : Error
    {
        public override string Code => "MockNameError.Length";

        public MockNameError(string message) : base(message)
        {
        }
    }
}
