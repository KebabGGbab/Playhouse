using Playhouse.Domain.SharedKernel.BaseModels;

namespace Playhouse.Domain.SharedKernel.Test.Mocks
{
    internal sealed class MockEntity : Entity
    {
        public string Name { get; }

        public int Age { get; }

        public MockEntity(int id, string name, int age)
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
