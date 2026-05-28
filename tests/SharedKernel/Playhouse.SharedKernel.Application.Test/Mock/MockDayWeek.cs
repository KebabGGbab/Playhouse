using Ardalis.SmartEnum;

namespace Playhouse.SharedKernel.Application.Test.Mock
{
    internal sealed class MockDayWeek : SmartEnum<MockDayWeek>
    {
        public static MockDayWeek Monday = new(nameof(Monday), 1);
        public static MockDayWeek Tuesday = new(nameof(Tuesday), 2);
        public static MockDayWeek Wednesday = new(nameof(Wednesday), 3);
        public static MockDayWeek Thursday = new(nameof(Thursday), 4);
        public static MockDayWeek Friday = new(nameof(Friday), 5);
        public static MockDayWeek Saturday = new(nameof(Saturday), 6);
        public static MockDayWeek Sunday = new(nameof(Sunday), 7);

        public MockDayWeek(string name, int value) : base(name, value)
        {
        }
    }
}
