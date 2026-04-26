using Playhouse.SharedKernel.Domain.Exceptions;
using Playhouse.SharedKernel.Domain.Test.Mocks;

namespace Playhouse.SharedKernel.Domain.Test.TestClasses
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
        public void AddDomainEvent_DomainEventIsNull_Throw()
        {
            MockAggregateRoot aggregateRoot = new();

            void action() => aggregateRoot.AddNullEvent();

            Assert.ThrowsExactly<ArgumentNullException>(action);
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
