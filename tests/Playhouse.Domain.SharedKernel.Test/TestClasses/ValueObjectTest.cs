using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses
{
    [TestClass]
    public sealed class ValueObjectTest
    {
        [TestMethod]
        public void Equals_OtherIsNull_ObjectsAreNotEqual()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject? mockOther = null;

            bool isEqual = mock.Equals(mockOther);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void Equals_ValueObjectsDifferentType_ObjectsAreNotEqual()
        {
            MockValueObject mock = new("value", 1);
            ValueObjectMock2 mockOther = new("value", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void Equals_ValueObjectsWithEqualProperties_ObjectsAreEqual()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Equals_ValueObjectsWithNotEqualProperties_ObjectsAreNotEqual()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value2", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void GetHashCode_ValueObjectsWithEqualProperties_HashCodesAreEqual()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void GetHashCode_ValueObjectsWithNotEqualProperties_HashCodesAreNotEqual()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value2", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void MethodGetHashCodeReturnEqualValueWhenMethodEqualReturnTrue()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value", 1);

            bool isEquals = mock.Equals(mockOther);
            int hashCode = mock.GetHashCode();
            int otherHashCode = mockOther.GetHashCode();

            Assert.IsTrue(isEquals);
            Assert.AreEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void EqualsOperator_ValueObjectsAreNull_ObjectsAreEqual()
        {
            MockValueObject? mock = null;
            MockValueObject? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void EqualsOperator_OneValueObjectIsNull_ObjectsAreNotEqual()
        {
            MockValueObject? mock = new("value", 1);
            MockValueObject? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_ValueObjectsWithNotEqualProperties_ValueObjectsAreNotEqual()
        {
            MockValueObject? mock = new("value", 1);
            MockValueObject? otherMock = new("value", 2);

            bool isEquals = mock != otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_ValueObjectsWithEqualProperties_ValueObjectsAreEqual()
        {
            MockValueObject? mock = new("value", 1);
            MockValueObject? otherMock = new("value", 1);

            bool isEquals = mock != otherMock;

            Assert.IsFalse(isEquals);
        }
    }
}
