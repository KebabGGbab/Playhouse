using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services;
using Playhouse.Core.Services.PlaywrightService;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class PlaywrightExtensions
    {
        public static IServiceCollection AddPlaywright(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IPlaywrightBrowserInstaller, PlaywrightBrowserInstaller>()
                .AddSingleton<IInitializer, PlaywrightBrowserInstaller>((s) => (PlaywrightBrowserInstaller)s.GetRequiredService<IPlaywrightBrowserInstaller>())
                .AddSingleton<IPlaywrightFactory, PlaywrightFactory>();
        }
    }
}
