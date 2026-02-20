using CommunityToolkit.Mvvm.Messaging.Messages;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Messages
{
    public class GetBrowserEventsMessage : AsyncRequestMessage<BotInfoViewModel>
    {
        public BotInfoViewModel BotInfo { get; }

        public BrowserProfileViewModel BrowserProfile { get; }

        public GetBrowserEventsMessage(BotInfoViewModel botInfo, BrowserProfileViewModel browserProfile)
        {
            ArgumentNullException.ThrowIfNull(botInfo, nameof(botInfo));
            ArgumentNullException.ThrowIfNull(browserProfile, nameof(browserProfile));

            BotInfo = botInfo;
            BrowserProfile = browserProfile;
        }
    }
}
