using System.Globalization;
using KebabGGbab.Localization;
using Microsoft.Extensions.DependencyInjection;
using Playhouse.Settings.UI.Resources;

namespace Playhouse.Settings.UI.Services.Localization
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizationSettings(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddLocalization();
            services.AddResxLocalization(StringsUI.ResourceManager, [CultureInfo.GetCultureInfo("en-US"), CultureInfo.GetCultureInfo("ru-RU")]);

            return services;
        }
    }
}
