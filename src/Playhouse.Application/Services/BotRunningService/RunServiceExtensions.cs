using Microsoft.Extensions.DependencyInjection;

namespace Playhouse.Application.Services.BotRunningService
{
    public static class RunServiceExtensions
    {
        public static IServiceCollection AddRunning(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IRunService, RunService>()
                .AddSingleton<IRunServiceFactory, RunServiceFactory>(); 
        }
    }
}
