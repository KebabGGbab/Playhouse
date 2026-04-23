using Playhouse.Domain.SharedKernel.Results;
using Playhouse.Domain.SharedKernel.Specifications.Validation;
using Playhouse.Domain.SharedKernel.Test.Resources.Strings;

namespace Playhouse.Domain.SharedKernel.Test.Mocks
{
    internal sealed class MockLeghtNameValidationSpecification : IValidationSpecification<string>
    {
        public Result IsSatisfiedBy(string entity)
        {
            if (entity.Length is <= 3 or >= 50)
            {
                return Result.Fail([new MockNameError(ExceptionMessages.MockLenghtNameValidationSpecification)]);
            }

            return Result.Ok();
        }
    }

    internal sealed class MockEmptyNameValidationSpecification : IValidationSpecification<string>
    {
        public Result IsSatisfiedBy(string entity)
        {
            if (string.IsNullOrWhiteSpace(entity))
            {
                return Result.Fail([new MockNameError(ExceptionMessages.MockEmptyNameValidationSpecification)]);
            }

            return Result.Ok();
        }
    }

    internal sealed class MockContainsOnlyLettersValidationSpecification : IValidationSpecification<string>
    {
        public Result IsSatisfiedBy(string entity)
        {
            if (entity.Any(c => !char.IsLetter(c)))
            {
                return Result.Fail([new MockNameError(ExceptionMessages.MockContainsOnlyLettersValidationSpecification)]);
            }

            return Result.Ok();
        }
    }
}
