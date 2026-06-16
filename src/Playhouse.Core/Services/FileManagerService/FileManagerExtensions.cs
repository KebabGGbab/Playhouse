using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Domain;

namespace Playhouse.Core.Services.FileManagerService
{
    public static class FileManagerExtensions
    {
        public static IServiceCollection AddFileManagers(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<FileManager<BrowserConfiguration>, BrowserProfileFileManager>();
        }
    }
}
