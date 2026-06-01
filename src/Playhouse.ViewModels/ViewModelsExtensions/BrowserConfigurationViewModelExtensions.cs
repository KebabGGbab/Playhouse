using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.ViewModelsExtensions
{
    public static class BrowserConfigurationViewModelExtensions
    {
        public static bool FilterByName(this BrowserConfigurationViewModel browser, string mask)
        {
            return browser.Name.Contains(mask);
        }
    }
}
