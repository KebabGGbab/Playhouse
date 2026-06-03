using CommunityToolkit.Mvvm.Messaging.Messages;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Messages
{
    public class GetBotActionsMessage : AsyncRequestMessage<BotConfigurationViewModel>
    {
        public BotConfigurationViewModel Bot { get; }

        public BrowserConfigurationViewModel Profile { get; }

        public GetBotActionsMessage(BotConfigurationViewModel botInfo, BrowserConfigurationViewModel browserProfile)
        {
            ArgumentNullException.ThrowIfNull(botInfo, nameof(botInfo));
            ArgumentNullException.ThrowIfNull(browserProfile, nameof(browserProfile));

            Bot = botInfo;
            Profile = browserProfile;
        }
    }
}
