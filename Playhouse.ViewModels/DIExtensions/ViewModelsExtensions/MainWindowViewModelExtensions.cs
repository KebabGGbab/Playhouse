using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.DIExtensions.ViewModelsExtensions
{
    public static class MainWindowViewModelExtensions
    {
        public static void AddMainWindowViewModel(this ServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.TryAddSingleton<UpdateViewModel>();
            services.TryAddSingleton<RunViewModel>();
            services.TryAddSingleton<BotsInfoViewModel>();
            services.TryAddSingleton<SettingsViewModel>();
        }
    }
}
