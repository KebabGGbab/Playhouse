using Playhouse.Core.Models;

namespace Playhouse.ViewModels.CoreExtensions.Models
{
    public static class BotInfoExtensions
    {
        public static bool FilterByName(this BotInfo bot, string mask)
        {
            return bot.Name.Contains(mask);
        }
    }
}
