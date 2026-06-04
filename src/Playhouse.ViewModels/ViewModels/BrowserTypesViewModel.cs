using Playhouse.Core.Models;
using Playhouse.ViewModels.ViewModels.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class BrowserTypesViewModel : SelectableItemViewModel<string>
    {
        internal BrowserTypes Browser { get; }

        public IReadOnlySet<BrowserChannelsViewModel> Channels { get; }

        public BrowserTypesViewModel(BrowserTypes browser) : base(browser.Name)
        {
            ArgumentNullException.ThrowIfNull(browser);

            Browser = browser;
            Channels = new HashSet<BrowserChannelsViewModel>(browser.Channels.Select(c => new BrowserChannelsViewModel(this, c)));
        }
    }
}
