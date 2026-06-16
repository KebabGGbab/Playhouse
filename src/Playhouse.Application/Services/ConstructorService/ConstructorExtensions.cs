using Microsoft.Extensions.DependencyInjection;
using Playhouse.Application.Services.ConstructorService.Abstractions;

namespace Playhouse.Application.Services.ConstructorService
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
