using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.PlaywrightService;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class PlaywrightExtensions
    {
        public static IServiceCollection AddPlaywright(this IServiceCollection services)
        {
            return services.AddSingleton<IPlaywrightBrowserInstaller, PlaywrightBrowserInstaller>()
                .AddSingleton<IPlaywrightFactory, PlaywrightFactory>();
        }
    }
}
