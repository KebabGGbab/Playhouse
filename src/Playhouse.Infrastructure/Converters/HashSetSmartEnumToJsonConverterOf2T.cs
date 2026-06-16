using Ardalis.SmartEnum;

namespace Playhouse.Infrastructure.Converters
{
    internal class HashSetSmartEnumToJsonConverter<TEnum> : HashSetSmartEnumToJsonConverter<TEnum, int>
        where TEnum : SmartEnum<TEnum, int>
    {
    }
}
