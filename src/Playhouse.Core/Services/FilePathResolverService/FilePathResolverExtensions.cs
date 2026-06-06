using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.FilePathResolverService
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
