using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class EntitySettingsTest
    {
        [TestMethod]
        public void Default()
        {
            string defaultEntityName = "Безымянный";

            EntitySettings entitySettings = EntitySettings.Default;

            Assert.AreEqual(defaultEntityName, entitySettings.DefaultName);
        }

        [TestMethod]
        public void Create_Simple_ResultOk()
        {
            string entityDefaultName = "Entity";

            EntitySettings entitySettings = EntitySettings.Create(entityDefaultName).Value!;

            Assert.AreEqual(entityDefaultName, entitySettings.DefaultName);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void Create_EntityDefaultNameIsNullOrWhiteSpace_ResultFail(string? entityDefaultName)
        {
            Result<EntitySettings> result = EntitySettings.Create(entityDefaultName!);

            Assert.HasCount(1, result.Errors!);
            Assert.Contains("Имя сущности по умолчанию не задано.", result.Errors!);
        }

        [TestMethod]
        public void Create_DefaultEntityNameLenghtExceedsMaximumLimit_ResultFail()
        {
            string entityDefaultName = new('e', 201);

            Result<EntitySettings> result = EntitySettings.Create(entityDefaultName!);

            Assert.Contains("Длина имени сущности по умолчанию не может превышать 200 символов.", result.Errors!);
        }
    }
}
