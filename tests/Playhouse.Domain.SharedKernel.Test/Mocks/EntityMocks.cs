using Playhouse.Domain.SharedKernel.SeedWork;

namespace Playhouse.Domain.SharedKernel.Test.Mocks
{
    internal sealed class EntityMock : Entity
    {
        public string Name { get; }

        public int Age { get; }

        public EntityMock(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    internal sealed class EntityMock2 : Entity
    {
        public EntityMock2(int id)
        {
            Id = id;
        }
    }
}
