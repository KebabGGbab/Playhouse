using Microsoft.Extensions.DependencyInjection;

namespace Playhouse.Core.Services.ApplicationSettingsService
{
    public static class SettingsExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSingleton<ISettingsService, SettingsService>();

            return services;
        }
    }
}
