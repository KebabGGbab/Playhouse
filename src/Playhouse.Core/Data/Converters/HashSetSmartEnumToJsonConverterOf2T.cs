using Ardalis.SmartEnum;

namespace Playhouse.Core.Data.Converters
{
    internal class HashSetSmartEnumToJsonConverter<TEnum> : HashSetSmartEnumToJsonConverter<TEnum, int>
        where TEnum : SmartEnum<TEnum, int>
    {
    }
}
