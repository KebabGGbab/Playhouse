using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses
{
    [TestClass]
    public sealed class EntityTest
    {
        [TestMethod]
        public void Equals_OtherIsNull_False()
        {
            MockEntity mock = new(1, "name", 20);
            MockEntity? otherMock = null;

            bool isEquals = mock.Equals(otherMock);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void Equals_SameReference_True()
        {
            MockEntity mock = new(1, "name", 20);

            bool isEquals = mock.Equals(mock);

            Assert.IsTrue(isEquals);
        }

        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [TestMethod]
        public void Equals_AnyObjectIsTransient_False(int id, int idOther)
        {
            MockEntity mock = new(id, "name", 20);
            MockEntity otherMock = new(idOther, "name", 20);

            bool isEquals = mock.Equals(otherMock);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void Equals_SameIdAndDifferentState_True()
        {
            MockEntity mock = new(1, "name", 20);
            MockEntity otherMock = new(1, "otherName", 21);

            bool isEquals = mock.Equals(otherMock);

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void Equals_DifferentIdsAndSameState_False()
        {
            MockEntity mock = new(1, "name", 20);
            MockEntity otherMock = new(2, "name", 20);

            bool isEquals = mock.Equals(otherMock);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void Equals_DifferentTypes_False()
        {
            MockEntity mock = new(1, "name", 20);
            EntityMock2 mock2 = new(1);

            bool isEquals = mock.Equals(mock2);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void IsTransient_IdIsDefault_True()
        {
            MockEntity mock = new(default, "name", 20);

            bool isTransient = mock.IsTransient();

            Assert.IsTrue(isTransient);
        }

        [TestMethod]
        public void IsTransient_IdIsNotDefault_False()
        {
            MockEntity mock = new(1, "name", 20);

            bool isTransient = mock.IsTransient();

            Assert.IsFalse(isTransient);
        }

        [TestMethod]
        public void GetHashCode_SameId_HashCodesEqual()
        {
            MockEntity mock = new(1, "name", 20);
            MockEntity otherMock = new(1, "otherName", 21);

            int hashCode = mock.GetHashCode();
            int otherHashCode = otherMock.GetHashCode();

            Assert.AreEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void GetHashCode_DifferentIds_HashCodesDifferent()
        {
            MockEntity mock = new(1, "name", 20);
            MockEntity otherMock = new(2, "otherName", 21);

            int hashCode = mock.GetHashCode();
            int otherHashCode = otherMock.GetHashCode();

            Assert.AreNotEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void GethashCode_TransientEntities_HashCodesDifferent()
        {
            MockEntity mock = new(0, "name", 20);
            MockEntity otherMock = new(0, "otherName", 21);

            int hashCode = mock.GetHashCode();
            int otherHashCode = otherMock.GetHashCode();

            Assert.AreNotEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void EqualsOperator_BothNull_True()
        {
            MockEntity? mock = null;
            MockEntity? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void EqualsOperator_OneNull_False()
        {
            MockEntity? mock = new(1, "name", 20);
            MockEntity? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_DifferentIds_True()
        {
            MockEntity? mock = new(1, "name", 20);
            MockEntity? otherMock = new(2, "name", 20);

            bool isEquals = mock != otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_Same_False()
        {
            MockEntity? mock = new(1, "name", 20);
            MockEntity? otherMock = new(1, "name", 20);

            bool isEquals = mock != otherMock;

            Assert.IsFalse(isEquals);
        }
    }
}
