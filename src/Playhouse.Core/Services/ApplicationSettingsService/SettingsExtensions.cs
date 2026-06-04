using Microsoft.Extensions.DependencyInjection;

namespace Playhouse.Core.Services.ApplicationSettingsService
{
    public static class SettingsExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<ISettingsService, SettingsService>()
                .AddSingleton<IInitializer, SettingsService>((s) => (SettingsService)s.GetRequiredService<ISettingsService>());
        }
    }
}
