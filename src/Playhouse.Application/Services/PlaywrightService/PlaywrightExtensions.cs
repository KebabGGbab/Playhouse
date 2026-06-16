using Microsoft.Extensions.DependencyInjection;
using Playhouse.Application.Services;
using Playhouse.Application.Services.PlaywrightService.Abstractions;

namespace Playhouse.Application.Services.PlaywrightService
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
