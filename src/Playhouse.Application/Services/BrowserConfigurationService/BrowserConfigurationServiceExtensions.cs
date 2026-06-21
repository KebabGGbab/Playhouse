using Microsoft.Extensions.DependencyInjection;
using Playhouse.Domain;

namespace Playhouse.Application.Services.BrowserConfigurationService
{
    public static class BrowserConfigurationServiceExtensions
    {
        public static async Task SaveAsync(this IBrowserConfigurationService service, BrowserConfiguration configuration, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(service);

            await service.SaveAsync([configuration], cancellation)
                .ConfigureAwait(false);
        }

        public static IServiceCollection AddBrowserConfigurationService(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IBrowserConfigurationService, BrowserConfigurationService>()
                .AddSingleton<IInitializer, BrowserConfigurationService>((s) => (BrowserConfigurationService)s.GetRequiredService<IBrowserConfigurationService>());
        }
    }
}
