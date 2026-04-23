using Playhouse.Domain.SharedKernel.Exceptions;
using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses
{
    [TestClass]
    public sealed class AggregateRootTest
    {
        [TestMethod]
        public void AddDomainEvent_Simple_EventsAdded()
        {
            MockAggregateRoot aggregateRoot = new();

            aggregateRoot.DoOperation();
            aggregateRoot.DoOtherOperation();

            Assert.HasCount(2, aggregateRoot.Events);
        }

        [TestMethod]
        public void AddDomainEvent_DomainEventIsNull_EventsAdded()
        {
            MockAggregateRoot aggregateRoot = new();

            void action() => aggregateRoot.AddNullEvent();

            Assert.ThrowsExactly<DomainEventIsNullDomainException>(action);
        }

        [TestMethod]
        public void ClearDomainEvents_EventsEmpty_NoExceptions()
        {
            MockAggregateRoot aggregateRoot = new();

            aggregateRoot.ClearDomainEvents();

            Assert.HasCount(0, aggregateRoot.Events);
        }

        [TestMethod]
        public void ClearDomainEvents_EventsNotEmpty_DomainEventCollectionCleared()
        {
            MockAggregateRoot aggregateRoot = new();
            aggregateRoot.DoOperation();

            aggregateRoot.ClearDomainEvents();

            Assert.HasCount(0, aggregateRoot.Events);
        }
    }
}
