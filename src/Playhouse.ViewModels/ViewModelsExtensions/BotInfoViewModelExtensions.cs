using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.ViewModelsExtensions
{
    public static class BotInfoViewModelExtensions
    {
        public static bool FilterByName(this BotInfoViewModel bot, string mask)
        {
            return bot.Name.Contains(mask);
        }
    }
}
