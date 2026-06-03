using Microsoft.Extensions.DependencyInjection;
using Playhouse.UI.Services.LocalizationService;

namespace Playhouse.Core.Services.LocalizationService
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizator(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSingleton<ILocalizator, Localizator>();

            return services;
        }
    }
}
