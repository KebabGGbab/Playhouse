using Playhouse.Core.Models;

namespace Playhouse.ViewModels.ModelsExtensions
{
    public static class BotInfoExtensions
    {
        public static bool FilterByName(this BotInfo bot, string mask)
        {
            return bot.Name.Contains(mask);
        }
    }
}
