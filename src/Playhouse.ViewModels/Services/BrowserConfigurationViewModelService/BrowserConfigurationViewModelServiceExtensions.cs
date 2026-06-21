using Microsoft.Extensions.DependencyInjection;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.BrowserConfigurationViewModelService
{
    public static class BrowserConfigurationViewModelServiceExtensions
    {
        public static async Task SaveAsync(this IBrowserConfigurationViewModelService service, BrowserConfigurationViewModel configuration, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(service);

            await service.SaveAsync([configuration], cancellation)
                .ConfigureAwait(false);
        }

        public static IServiceCollection AddBrowserConfigurationViewModelService(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IBrowserConfigurationViewModelService, BrowserConfigurationViewModelService>();
        }
    }
}
