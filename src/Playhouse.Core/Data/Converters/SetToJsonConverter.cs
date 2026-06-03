using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Playhouse.Core.Data.Converters
{
    internal sealed class SetToJsonConverter<TEnumerable> : ValueConverter<TEnumerable, string>
    {
        public SetToJsonConverter() 
            : base(
                  v => ConvertToJson(v),
                  v => ConvertFromJson(v))
        {
        }

        public static string ConvertToJson(TEnumerable enums)
        {
            return JsonSerializer.Serialize(enums);
        }

        public static TEnumerable ConvertFromJson(string json)
        {
            return JsonSerializer.Deserialize<TEnumerable>(json);
        }
    }
}
