using Microsoft.Extensions.DependencyInjection;
using Playhouse.Domain;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.ViewModelFactories
{
    public static class ViewModelFactoriesExtensions
    {
        public static IServiceCollection AddViewModelFactories(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IViewModelFactory<BrowserConfigurationViewModel, BrowserConfiguration>, BrowserConfigurationViewModelFactory>()
                .AddSingleton<IViewModelFactory<BotConfigurationViewModel, BotConfiguration>, BotConfigurationViewModelFactory>();
        }
    }
}
