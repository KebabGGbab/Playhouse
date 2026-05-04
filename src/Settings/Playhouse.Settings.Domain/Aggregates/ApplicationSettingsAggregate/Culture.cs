using Playhouse.SharedKernel.Domain.BaseModels;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.Culture;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate
{
    public sealed class Culture : ValueObject
    {
        private const string DEFAULT_CODE = "en";

        private static readonly CultureCodeNotEmptyValidationSpecification _emptyValidation;
        private static readonly CodeValidationSpecification _codeValidation;

        public static Culture Default { get; }

        public static IEnumerable<string> SupportedCultures { get; }

        static Culture() 
        {
            SupportedCultures = [DEFAULT_CODE, "ru"];
            Default = new(DEFAULT_CODE);
            _codeValidation = new CodeValidationSpecification(SupportedCultures);
            _emptyValidation = new CultureCodeNotEmptyValidationSpecification();
        }

        public string Code { get; }

        private Culture(string name) 
        {
            Code = name;
        }

        public static Result CanCreate(string code)
        {
            Result resultEmptyValidation = _emptyValidation.IsSatisfiedBy(code);

            if (resultEmptyValidation.IsFailure)
            {
                return resultEmptyValidation;
            }

            code = TransformCode(code);

            return _codeValidation.IsSatisfiedBy(code);
        }

        public static Result<Culture> Create(string code)
        {
            Result canCreate = CanCreate(code);

            if (canCreate.IsFailure)
            {
                return Result.Fail<Culture>(canCreate.Errors);
            }

            code = TransformCode(code);

            return Result.Ok(new Culture(code));
        }

        private static string TransformCode(string code)
        {
            return code.Trim()
                .ToLowerInvariant();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
