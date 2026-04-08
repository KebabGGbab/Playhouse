using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses
{
    [TestClass]
    public sealed class EntityTest
    {
        [TestMethod]
        public void Equals_OtherIsNull_ObjectsAreNotEqual()
        {
            EntityMock mock = new(1, "name", 20);
            EntityMock? otherMock = null;

            bool isEquals = mock.Equals(otherMock);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void Equals_ReferenceEquals_ObjectsAreEqual()
        {
            EntityMock mock = new(1, "name", 20);

            bool isEquals = mock.Equals(mock);

            Assert.IsTrue(isEquals);
        }

        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [TestMethod]
        public void Equals_AnyObjectIsTransient_ObjectsAreNotEqual(int id, int idOther)
        {
            EntityMock mock = new(id, "name", 20);
            EntityMock otherMock = new(idOther, "name", 20);

            bool isEquals = mock.Equals(otherMock);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void Equals_EqualIdAndNotEqualOtherProperties_ObjectsAreEqual()
        {
            EntityMock mock = new(1, "name", 20);
            EntityMock otherMock = new(1, "otherName", 21);

            bool isEquals = mock.Equals(otherMock);

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void Equals_NotEqualIdAndEqualsOtherProperties_ObjectsAreNotEqual()
        {
            EntityMock mock = new(1, "name", 20);
            EntityMock otherMock = new(2, "name", 20);

            bool isEquals = mock.Equals(otherMock);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void Equals_EntitiesDifferentType_ObjectsAreNotEqual()
        {
            EntityMock mock = new(1, "name", 20);
            EntityMock2 mock2 = new(1);

            bool isEquals = mock.Equals(mock2);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void IsTransient_IdIsDefault_ObjectIsTransient()
        {
            EntityMock mock = new(default, "name", 20);

            bool isTransient = mock.IsTransient();

            Assert.IsTrue(isTransient);
        }

        [TestMethod]
        public void IsTransient_IdIsNotDefault_ObjectIsNotTransient()
        {
            EntityMock mock = new(1, "name", 20);

            bool isTransient = mock.IsTransient();

            Assert.IsFalse(isTransient);
        }

        [TestMethod]
        public void GetHashCode_EntitiesWithEqualIds_HashCodesEqual()
        {
            EntityMock mock = new(1, "name", 20);
            EntityMock otherMock = new(1, "otherName", 21);

            int hashCode = mock.GetHashCode();
            int otherHashCode = otherMock.GetHashCode();

            Assert.AreEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void GetHashCode_EntitiesWithNotEqualIds_HashCodesAreEqual()
        {
            EntityMock mock = new(1, "name", 20);
            EntityMock otherMock = new(2, "otherName", 21);

            int hashCode = mock.GetHashCode();
            int otherHashCode = otherMock.GetHashCode();

            Assert.AreNotEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void MethodGetHashCodeReturnEqualValueWhenMethodEqualReturnTrue()
        {
            EntityMock mock = new(1, "name", 20);
            EntityMock otherMock = new(1, "otherName", 21);

            bool isEquals = mock.Equals(otherMock);
            int hashCode = mock.GetHashCode();
            int otherHashCode = otherMock.GetHashCode();

            Assert.IsTrue(isEquals);
            Assert.AreEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void EqualsOperator_ReferenceAreNull_EntitiesAreEqual()
        {
            EntityMock? mock = null;
            EntityMock? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void EqualsOperator_OneReferenceIsNull_EntitiesAreNotEqual()
        {
            EntityMock? mock = new(1, "name", 20);
            EntityMock? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_EntitiesWithNotEqualId_EntitiesAreNotEqual()
        {
            EntityMock? mock = new(1, "name", 20);
            EntityMock? otherMock = new(2, "name", 20);

            bool isEquals = mock != otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_EntitiesWithEqualId_EntitiesAreEqual()
        {
            EntityMock? mock = new(1, "name", 20);
            EntityMock? otherMock = new(1, "name", 20);

            bool isEquals = mock != otherMock;

            Assert.IsFalse(isEquals);
        }
    }
}
