using Playhouse.Domain;
using Playhouse.ViewModels.ViewModels.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class BrowserChannelsViewModel : SelectableItemViewModel<string>
    {
        internal BrowserChannels Channel { get; }

        public BrowserTypesViewModel Browser { get; }

        public BrowserChannelsViewModel(BrowserTypesViewModel browser, BrowserChannels channel)
            : base(channel.Name)
        {
            ArgumentNullException.ThrowIfNull(browser);

            Channel = channel;
            Browser = browser;
        }
    }
}
