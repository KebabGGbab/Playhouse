using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Playhouse.ViewModels.ViewModels
{
    public static class ViewModelExtensions
    {
        public static void AddMainWindowViewModel(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.TryAddSingleton<UpdateViewModel>();
            services.TryAddSingleton<RunViewModel>();
            services.TryAddSingleton<BrowserConfigurationsViewModel>();
            services.TryAddSingleton<BotConfigurationsViewModel>();
            services.TryAddSingleton<SettingsViewModel>();
            services.TryAddSingleton<MainWindowViewModel>();
        }
    }
}
