using Microsoft.Extensions.DependencyInjection;
using Playhouse.Application.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Application.Services.FilePathResolverService
{
    public static class FilePathResolverExtensions
    {
        public static IServiceCollection AddFilePathResolver(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IFilePathResolver, FilePathResolver>();
        }
    }
}
