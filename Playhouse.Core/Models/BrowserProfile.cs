using Playhouse.Core.Models.PlaywrightDecorator;
using Playhouse.Core.Enums;

namespace Playhouse.Core.Models
{
	public class BrowserProfile
    {
        public BrowserTypeLaunchPersistentContextOptionsStrictDecorator Options { get; init; } = null!;

        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        // Конструктор для EntityFramework
        private BrowserProfile()
        {
        }

        public BrowserProfile(BrowserTypeLaunchPersistentContextOptionsStrictDecorator? options = null)
        {
            Options = options ?? new BrowserTypeLaunchPersistentContextOptionsStrictDecorator();
        }

        public override string ToString() => $"{$"[{Id}]",-8}{Name}";
    }
}