using Microsoft.Playwright;

namespace Playhouse.Core.Models
{
	public class BrowserProfile : BrowserTypeLaunchPersistentContextOptions
    {
        public int Id { get; private set; }

        public required string Name { get; set; }

        public override string ToString() => $"{$"[{Id}]",-8}{Name}";
    }
}