using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.BotRunningService;
using Playhouse.Core.Services.BotRunningService.Abstrtactions;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class BotRunningServiceExtensions
    {
        public static IServiceCollection AddBotRunning(this IServiceCollection services)
        {
            return services.AddSingleton<IBotJobManagerFactory, BotJobManagerFactory>(); 
        }
    }
}
