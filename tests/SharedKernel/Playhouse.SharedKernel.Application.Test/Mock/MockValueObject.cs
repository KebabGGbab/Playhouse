using Playhouse.SharedKernel.Application.Test.Resources.Strings;
using Playhouse.SharedKernel.Domain.BaseModels;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.SharedKernel.Application.Test.Mock
{
    internal sealed class MockValueObject : ValueObject
    {
        public string Name { get; }

        private MockValueObject(string name)
        {
            Name = name;
        }

        public static Result<MockValueObject> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<MockValueObject>.Fail(ErrorMessages.MockValueObjectNameIsNull);
            }

            if (name.Length < 3)
            {
                return Result<MockValueObject>.Fail(ErrorMessages.MockValueObjectShortName);
            }

            return Result<MockValueObject>.Ok(new MockValueObject(name));

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
