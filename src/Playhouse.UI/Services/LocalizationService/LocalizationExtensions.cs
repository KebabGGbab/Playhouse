using System.Globalization;
using KebabGGbab.Localization;
using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.LocalizationService;
using Playhouse.UI.Resources.Localization;

namespace Playhouse.UI.Services.LocalizationService
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizationApp(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddLocalization();
            services.AddResxLocalization(StringsUI.ResourceManager, [CultureInfo.GetCultureInfo("ru-RU"), CultureInfo.GetCultureInfo("en-US")]);
            services.AddLocalizator();

            return services;
        }
    }
}
