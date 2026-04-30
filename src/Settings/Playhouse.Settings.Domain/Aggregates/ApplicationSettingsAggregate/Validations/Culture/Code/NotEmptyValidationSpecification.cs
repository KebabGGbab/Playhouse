using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.Culture;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.Culture.Code
{
    public sealed class NotEmptyValidationSpecification : IValidationSpecification<string>
    {
        public Result IsSatisfiedBy(string code)
        {
            return string.IsNullOrWhiteSpace(code)
                ? Result.Fail([new CultureCodeEmptyError()])
                : Result.Ok();
        }
    }
}
