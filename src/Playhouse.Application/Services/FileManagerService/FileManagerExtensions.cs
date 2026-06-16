using Microsoft.Extensions.DependencyInjection;
using Playhouse.Application.Services.FileManagerService.Abstractions;
using Playhouse.Domain;

namespace Playhouse.Application.Services.FileManagerService
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
