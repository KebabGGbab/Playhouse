using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.ViewModelsExtensions
{
    public static class BotConfigurationViewModelExtensions
    {
        public static bool FilterByName(this BotConfigurationViewModel bot, string mask)
        {
            return bot.Name.Contains(mask);
        }
    }
}
