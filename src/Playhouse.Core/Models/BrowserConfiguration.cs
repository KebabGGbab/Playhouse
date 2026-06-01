using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models
{
	public class BrowserConfiguration
    {
        public BrowserTypeLaunchPersistentContextOptionsStrictDecorator Options { get; init; } = null!;

        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        // Конструктор для EntityFramework
        private BrowserConfiguration()
        {
        }

        public BrowserConfiguration(BrowserTypeLaunchPersistentContextOptionsStrictDecorator? options = null)
        {
            Options = options ?? new BrowserTypeLaunchPersistentContextOptionsStrictDecorator();
        }

        public override string ToString() => $"{$"[{Id}]",-8}{Name}";
    }
}