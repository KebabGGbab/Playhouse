using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.Resources.Strings;

namespace Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate
{
    public sealed class Culture : ValueObject
    {
        private const string DEFAULT_CULTURE = "en";

        public static Culture Default { get; } = new(DEFAULT_CULTURE);

        public static IEnumerable<string> SupportedCultures { get; } = [DEFAULT_CULTURE, "ru"]; 

        public string Name { get; }

        private Culture(string culture) 
        {
            Name = culture;
        }

        public static Result<Culture> Create(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture))
            {
                // Если культура не указана, то нет смысла продолжать собирать следующие ошибки, из-за чего сразу возвращаем ошибку.
                return Result<Culture>.Fail(ErrorMessages.CultureNotSpecified);
            }

            culture = culture.ToLowerInvariant();

            if (!SupportedCultures.Contains(culture))
            {
                return Result<Culture>.Fail(ErrorMessages.CultureIsNotSupported);
            }

            return Result<Culture>.Ok(new Culture(culture));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
