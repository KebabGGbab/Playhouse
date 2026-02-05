using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.BotRunningService;
using Playhouse.Core.Services.BotRunningService.Abstrtactions;

namespace Playhouse.ViewModels.CoreExtensions.Services
{
    public static class BotRunningServiceExtensions
    {
        public static IServiceCollection AddBotRunning(this IServiceCollection services)
        {
            return services.AddSingleton<IBotJobManagerFactory, BotJobManagerFactory>(); 
        }
    }
}
