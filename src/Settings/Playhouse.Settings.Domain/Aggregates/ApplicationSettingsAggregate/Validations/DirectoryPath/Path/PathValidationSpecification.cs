using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.DirectoryPath.Path
{
    public sealed class PathValidationSpecification : IValidationSpecification<string>
    {
        private readonly IValidationSpecification<string> _validations;

        public PathValidationSpecification()
        {
            _validations = new HasNotInvalidCharsValidationSpecifications()
                .And(new AbsoluteValidationSpecification());
        }

        public Result IsSatisfiedBy(string path)
        {
            return _validations.IsSatisfiedBy(path);
        }
    }
}
