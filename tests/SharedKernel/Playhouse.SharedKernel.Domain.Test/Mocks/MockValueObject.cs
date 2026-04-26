using Playhouse.SharedKernel.Domain.BaseModels;

namespace Playhouse.Domain.SharedKernel.Test.Mocks
{
    internal class MockValueObject : ValueObject
    {
        public string Value { get; }

        public int Count { get; }

        public MockValueObject(string value, int count)
        {
            Value = value;
            Count = count;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Count;
        }
    }

    internal class ValueObjectMock2 : ValueObject
    {
        public string Value { get; }

        public int Count { get; }

        public ValueObjectMock2(string value, int count)
        {
            Value = value;
            Count = count;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Count;
        }
    }
}
