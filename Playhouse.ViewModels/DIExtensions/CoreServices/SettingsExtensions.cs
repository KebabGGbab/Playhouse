using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.SettingsService;
using Playhouse.Core.Services.SettingsService.Abstractions;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class SettingsExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            return services.AddSingleton<ISettingsUpdater<UserSettings>, UserSettingsUpdater>();
        }
    }
}
