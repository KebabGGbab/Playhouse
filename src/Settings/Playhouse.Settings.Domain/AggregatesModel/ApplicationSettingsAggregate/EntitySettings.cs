using System.Globalization;
using System.Text;
using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.Resources.Strings;

namespace Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate
{
    public sealed class EntitySettings : ValueObject
    {
        private const int MAX_LENGHT = 200;
        
        private static readonly CompositeFormat _entitySettingsCannotExceedErrorMessage = CompositeFormat.Parse(ErrorMessages.EntitySettingsCannotExceed);

        // Каждый раз возвращаем новый объект из-за того, что культура могла измениться.
        public static EntitySettings Default => new(GetDefaultName());

        public string DefaultName { get; }

        private EntitySettings(string name) 
        {
            DefaultName = name;
        }

        public static Result<EntitySettings> Create(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                // Если имя не указано, то нет смысла продолжать собирать следующие ошибки, из-за чего сразу возвращаем ошибку.
                return Result<EntitySettings>.Fail(ErrorMessages.EntitySettingsDefaultNameNotSet);
            }

            name = name.Trim();

            if (name.Length > MAX_LENGHT)
            {
                return Result<EntitySettings>.Fail(string.Format(CultureInfo.CurrentCulture, _entitySettingsCannotExceedErrorMessage, MAX_LENGHT));
            }

            return Result<EntitySettings>.Ok(new EntitySettings(name));
        }

        private static string GetDefaultName()
        {
            return DefaultValues.EntitySettingsDefaultName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DefaultName;
        }
    }
}
