using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.ViewModelsExtensions
{
    public static class BrowserProfileViewModelExtensions
    {
        public static bool FilterByName(this BrowserProfileViewModel profile, string mask)
        {
            return profile.Name.Contains(mask);
        }
    }
}
