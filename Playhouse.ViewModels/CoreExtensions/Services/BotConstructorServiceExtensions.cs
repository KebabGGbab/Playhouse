using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.BotConstructorService;
using Playhouse.Core.Services.BotConstructorService.Abstractions;

namespace Playhouse.ViewModels.CoreExtensions.Services
{
    public static class BotConstructorServiceExtensions
    {
        public static IServiceCollection AddBotConstruction(this IServiceCollection services)
        {
            return services.AddSingleton<IBotConstructorFactory, BotConstructorFactory>();
        }
    }
}
