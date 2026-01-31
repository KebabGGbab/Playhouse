using Playhouse.Core.Enums;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models
{
	public class BotInfo
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public BrowserType Browser { get; set; }

        public IList<BrowserEvent> BrowserEvents { get; } = [];

        public override string ToString() => $"{$"[{Id}]",-8}{Name}";
    }
}
