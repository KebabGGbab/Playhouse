using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Playhouse.Core.Data.Converters
{
    internal class HashSetToJsonConverter<TItem> : ValueConverter<ISet<TItem>, string>
    {
        public HashSetToJsonConverter()
            : base(
                  v => ConvertToJson(v),
                  v => ConvertFromJson(v))
        {
        }

        public static string ConvertToJson(ISet<TItem> items)
        {
            return JsonSerializer.Serialize(items);
        }

        public static ISet<TItem> ConvertFromJson(string json)
        {
            return JsonSerializer.Deserialize<ISet<TItem>>(json) ?? new HashSet<TItem>();
        }
    }
}
