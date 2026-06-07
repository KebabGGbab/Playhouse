using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;

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
