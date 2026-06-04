using System.Text.Json;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.EFCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Playhouse.Core.Data.Converters
{
    internal class HashSetSmartEnumToJsonConverter<TEnum, TValue> : ValueConverter<ISet<TEnum>, string>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public HashSetSmartEnumToJsonConverter() 
            : base(
                  v => ConvertToJson(v),
                  v => ConvertFromJson(v))
        {
        }

        public static string ConvertToJson(ISet<TEnum> enums)
        {
            return JsonSerializer.Serialize(enums.Select(e => e.Value));
        }

        public static ISet<TEnum> ConvertFromJson(string json)
        {
            ISet<TValue>? values = JsonSerializer.Deserialize<ISet<TValue>>(json);

            if (values == null)
            {
                return new HashSet<TEnum>();
            }

            return values.Select(SmartEnumConverter<TEnum, TValue>.GetFromValue).ToHashSet();
        }
    }
}
