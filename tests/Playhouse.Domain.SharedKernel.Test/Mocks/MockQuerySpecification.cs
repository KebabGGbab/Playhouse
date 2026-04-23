using Playhouse.Domain.SharedKernel.Specifications.Query;

namespace Playhouse.Domain.SharedKernel.Test.Mocks
{
    internal sealed class MockRangeIdQuerySpecification : IQuerySpecification<MockEntity>
    {
        private readonly int _from;
        private readonly int _to;

        public MockRangeIdQuerySpecification(int from, int to)
        {
            _from = from;
            _to = to;
        }

        public bool IsSatisfiedBy(MockEntity candidate)
        {
            return candidate.Id >= _from && candidate.Id <= _to;
        }
    }

    internal sealed class MockNameStartWithQuerySpecification : IQuerySpecification<MockEntity>
    {
        private readonly string _pattern;

        public MockNameStartWithQuerySpecification(string pattern)
        {
            _pattern = pattern;
        }

        public bool IsSatisfiedBy(MockEntity candidate)
        {
            return candidate.Name.StartsWith(_pattern);
        }
    }
}
