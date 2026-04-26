using Playhouse.SharedKernel.Domain.BaseModels;

namespace Playhouse.SharedKernel.Domain.Test.Mocks
{
    internal class MockAggregateRoot : AggregateRoot
    {
        public void DoOperation()
        {
            AddDomainEvent(new MockDomainEvent());
        }

        public void DoOtherOperation()
        {
            AddDomainEvent(new MockDomainEvent2());
        }

        public void AddNullEvent()
        {
            AddDomainEvent(null!);
        }
    }
}
