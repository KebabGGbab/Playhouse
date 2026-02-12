using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class FilePathResolverExtensions
    {
        public static IServiceCollection AddFilePathResolver(this IServiceCollection services)
        {
            return services.AddSingleton<IFilePathResolver, FilePathResolver>();
        }
    }
}
