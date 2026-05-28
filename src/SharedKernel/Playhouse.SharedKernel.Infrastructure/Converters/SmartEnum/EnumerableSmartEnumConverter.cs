using System.Text.Json;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.EFCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Playhouse.SharedKernel.Infrastructure.Converters.SmartEnum
{
    public class EnumerableSmartEnumConverter<TEnum, TValue> : ValueConverter<IEnumerable<TEnum>, string>
        where TEnum : SmartEnum<TEnum, TValue> 
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        private static TValue GetEnumValue(TEnum @enum)
        {
            return @enum.Value;
        }

        public static string ConvertToJson(IEnumerable<TEnum> enums)
        {
            IEnumerable<TValue> values = enums.Select(GetEnumValue);

            return JsonSerializer.Serialize(values);
        }

        public static IEnumerable<TEnum> ConvertFromJson(string json)
        {
            IEnumerable<TValue>? values = JsonSerializer.Deserialize<IEnumerable<TValue>>(json);

            if (values == null)
            {
                return [];
            }

            return values.Select(SmartEnumConverter<TEnum, TValue>.GetFromValue);
        }

        public EnumerableSmartEnumConverter()
            : base(enums => ConvertToJson(enums), value => ConvertFromJson(value))
        {
        }
    }
}
