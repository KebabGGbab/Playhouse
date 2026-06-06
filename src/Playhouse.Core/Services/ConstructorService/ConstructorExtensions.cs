using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.ConstructorService.Abstractions;

namespace Playhouse.Core.Services.ConstructorService
{
    public static class ConstructorExtensions
    {
        public static IServiceCollection AddBotConstruction(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IConstructorFactory, ConstructorFactory>();
        }
    }
}
