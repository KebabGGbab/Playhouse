using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.BotRunningService.Abstrtactions;

namespace Playhouse.Core.Services.BotRunningService
{
    public static class BotRunningExtensions
    {
        public static IServiceCollection AddBotRunning(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IBotJobManagerFactory, BotJobManagerFactory>(); 
        }
    }
}
