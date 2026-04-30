using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.Culture.Code
{
    public sealed class CodeValidationSpecification : IValidationSpecification<string>
    {
        private readonly IValidationSpecification<string> _validators;

        public CodeValidationSpecification(IEnumerable<string> supportedCultures)
        {
            _validators = new NotSupportedValidationSpecification(supportedCultures);
        }

        public Result IsSatisfiedBy(string code)
        {
            return _validators.IsSatisfiedBy(code);
        }
    }
}
