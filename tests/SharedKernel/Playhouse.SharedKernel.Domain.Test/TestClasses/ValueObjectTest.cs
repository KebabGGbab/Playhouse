using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.SharedKernel.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class ValueObjectTest
    {
        [TestMethod]
        public void Equals_OtherIsNull_False()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject? mockOther = null;

            bool isEqual = mock.Equals(mockOther);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void Equals_DifferentTypes_False()
        {
            MockValueObject mock = new("value", 1);
            ValueObjectMock2 mockOther = new("value", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void Equals_EqualProperties_True()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Equals_NotEqualProperties_False()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value2", 1);

            bool isEqual = mock.Equals(mockOther);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void GetHashCode_EqualProperties_HashCodesEqual()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value", 1);

            int hashCode = mock.GetHashCode();
            int otherHashCode = mockOther.GetHashCode();

            Assert.AreEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void GetHashCode_NotEqualProperties_HashCodesNotEqual()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject mockOther = new("value2", 1);

            int hashCode = mock.GetHashCode();
            int otherHashCode = mockOther.GetHashCode();

            Assert.AreNotEqual(hashCode, otherHashCode);
        }

        [TestMethod]
        public void EqualsOperator_BothNull_True()
        {
            MockValueObject? mock = null;
            MockValueObject? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void EqualsOperator_OneNull_False()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject? otherMock = null;

            bool isEquals = mock == otherMock;

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_DifferentProperties_True()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject otherMock = new("value", 2);

            bool isEquals = mock != otherMock;

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void NotEqualsOperator_EqualProperties_False()
        {
            MockValueObject mock = new("value", 1);
            MockValueObject otherMock = new("value", 1);

            bool isEquals = mock != otherMock;

            Assert.IsFalse(isEquals);
        }
    }
}
