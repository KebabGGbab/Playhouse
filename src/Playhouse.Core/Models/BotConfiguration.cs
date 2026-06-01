using Playhouse.Core.Enums;
using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.Core.Models
{
	public class BotConfiguration
    {
        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public BrowserType Browser { get; init; }

        public IList<BotAction> Actions { get; } = [];

        public override string ToString() => $"{$"[{Id}]",-8}{Name}";
    }
}
