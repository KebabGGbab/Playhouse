using Playhouse.Core.Models;

namespace Playhouse.ViewModels.CoreExtensions.Models
{
    public static class BrowserProfileExtensions
    {
        public static bool FilterByName(this BrowserProfile profile, string mask)
        {
            return profile.Name.Contains(mask);
        }
    }
}
