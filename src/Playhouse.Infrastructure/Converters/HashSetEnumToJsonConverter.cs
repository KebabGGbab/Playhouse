using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Playhouse.Infrastructure.Converters
{
    internal sealed class HashSetEnumToJsonConverter<TEnum> : ValueConverter<ISet<TEnum>, string>
        where TEnum : struct
    {
        public HashSetEnumToJsonConverter()
            : base(
                  v => ConvertToJson(v),
                  v => ConvertFromJson(v))
        {
        }

        public static string ConvertToJson(ISet<TEnum> enums)
        {
            return JsonSerializer.Serialize(enums.Select(e => e.ToString()));
        }

        public static ISet<TEnum> ConvertFromJson(string json)
        {
            IEnumerable<string>? values = JsonSerializer.Deserialize<IEnumerable<string>>(json);

            if (values is null)
            {
                return new HashSet<TEnum>();
            }
            
            return values.Select(Enum.Parse<TEnum>).ToHashSet();
        }
    }
}
