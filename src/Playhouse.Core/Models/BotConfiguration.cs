using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.Core.Models
{
	public class BotConfiguration
    {
        public int Id { get; private set; }

        public string Name { get; set; } = null!;

        public BrowserTypes Browser { get; } = null!;

        public IList<BotAction> Actions { get; } = null!;

        private BotConfiguration()
        {
        }

        public BotConfiguration(BrowserTypes browser)
        {
            ArgumentNullException.ThrowIfNull(browser);

            Name = string.Empty;
            Browser = browser;
            Actions = [];
        }
    }
}
