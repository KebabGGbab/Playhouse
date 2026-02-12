using System.Globalization;
using System.Resources;
using KebabGGbab.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.ViewModels.Services.LocalizationService.Abstractions;

namespace Playhouse.ViewModels.Services.LocalizationService
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalization(this IServiceCollection services, ResourceManager resourceManager)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddOptions();
            services.TryAddSingleton<ILocalizator>(s =>
            {
                LocalizationManagerBuilder builder = new();
                builder.AddProvider(
                    new ResxLocalizationProvider(
                        resourceManager,
                        [
                            CultureInfo.GetCultureInfo("en"), 
                            CultureInfo.GetCultureInfo("ru")
                        ]));
                builder.SetUICulture(CultureInfo.GetCultureInfo(s.GetRequiredService<IOptions<CultureOptions>>().Value.Name));
                
                return new Localizator(builder.Build());
            });

            return services;
        }
    }
}
