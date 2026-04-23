using Playhouse.Domain.SharedKernel.Exceptions;
using Playhouse.Domain.SharedKernel.Specifications.Query;
using Playhouse.Domain.SharedKernel.Specifications.Validation;
using Playhouse.Domain.SharedKernel.Test.Mocks;

namespace Playhouse.Domain.SharedKernel.Test.TestClasses;

[TestClass]
public class QuerySpecificationTest
{
    private readonly MockRangeIdQuerySpecification _rangeIdFilter;
    private readonly MockNameStartWithQuerySpecification _nameStartWithFilter;

    public QuerySpecificationTest()
    {
        _rangeIdFilter = new(10, 15);
        _nameStartWithFilter = new("a");
    }

    [TestMethod]
    public void And_LeftRuleIsNull_Throw()
    {
        MockNameStartWithQuerySpecification leftRule = null!;

        void action() => leftRule.And(_rangeIdFilter);

        Assert.ThrowsExactly<QuerySpecificationIsNullDomainException>(action);
    }

    [TestMethod]
    public void And_RightRuleIsNull_Throw()
    {
        MockRangeIdQuerySpecification rightRule = null!;

        void action() => _nameStartWithFilter.And(rightRule);

        Assert.ThrowsExactly<QuerySpecificationIsNullDomainException>(action);
    }

    [TestMethod]
    public void And_ValidState_True()
    {
        MockEntity entity = new(11, "artem", 20);
        IQuerySpecification<MockEntity> validationRule = _nameStartWithFilter.And(_rangeIdFilter);

        bool result = validationRule.IsSatisfiedBy(entity);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow(9, "artem")]
    [DataRow(11, "Ю")]
    [DataRow(1, "1")]
    public void And_NotValidState_False(int id, string name)
    {
        MockEntity entity = new(id, name, 20);
        IQuerySpecification<MockEntity> validationRule = _nameStartWithFilter.And(_rangeIdFilter);

        bool result = validationRule.IsSatisfiedBy(entity);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Or_LeftRuleIsNull_Throw()
    {
        MockNameStartWithQuerySpecification leftRule = null!;

        void action() => leftRule.Or(_rangeIdFilter);

        Assert.ThrowsExactly<QuerySpecificationIsNullDomainException>(action);
    }

    [TestMethod]
    public void Or_RightRuleIsNull_Throw()
    {
        MockRangeIdQuerySpecification rightRule = null!;

        void action() => _nameStartWithFilter.Or(rightRule);

        Assert.ThrowsExactly<QuerySpecificationIsNullDomainException>(action);
    }

    [TestMethod]
    [DataRow(11, "artem")]
    [DataRow(9, "artem")]
    [DataRow(11, "Ю")]
    public void Or_ValidState_True(int id, string name)
    {
        MockEntity entity = new(id, name, 20);
        IQuerySpecification<MockEntity> validationRule = _nameStartWithFilter.Or(_rangeIdFilter);

        bool result = validationRule.IsSatisfiedBy(entity);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Or_NotValidState_False()
    {
        MockEntity entity = new(1, "1", 20);
        IQuerySpecification<MockEntity> validationRule = _nameStartWithFilter.Or(_rangeIdFilter);

        bool result = validationRule.IsSatisfiedBy(entity);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Not_RuleIsNull_Throw()
    {
        MockNameStartWithQuerySpecification leftRule = null!;

        void action() => leftRule.Not();

        Assert.ThrowsExactly<QuerySpecificationIsNullDomainException>(action);
    }

    [TestMethod]
    public void Not_ValidState_True()
    {
        MockEntity entity = new(11, "B", 20);
        IQuerySpecification<MockEntity> validationRule = _nameStartWithFilter.Not();

        bool result = validationRule.IsSatisfiedBy(entity);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Not_NotValidState_False()
    {
        MockEntity entity = new(11, "artem", 20);
        IQuerySpecification<MockEntity> validationRule = _nameStartWithFilter.Not();

        bool result = validationRule.IsSatisfiedBy(entity);

        Assert.IsFalse(result);
    }
}
