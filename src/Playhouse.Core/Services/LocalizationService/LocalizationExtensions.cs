using Microsoft.Extensions.DependencyInjection;

namespace Playhouse.Core.Services.LocalizationService
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizator(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<ILocalizator, Localizator>()
                .AddSingleton<IInitializer, Localizator>((s) => (Localizator)s.GetRequiredService<ILocalizator>());
        }
    }
}
