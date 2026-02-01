using Playhouse.Core.Enums;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models
{
	public class BotInfo
    {
        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public BrowserType Browser { get; init; }

        public IList<BrowserEvent> BrowserEvents { get; } = [];

        public override string ToString() => $"{$"[{Id}]",-8}{Name}";
    }
}
