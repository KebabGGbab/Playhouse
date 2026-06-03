using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService;
using Playhouse.Core.Services.FileManagerService.Abstractions;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class FileManagerExtensions
    {
        public static IServiceCollection AddFileManagers(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            return services.AddSingleton<FileManager<BrowserConfiguration>, BrowserProfileFileManager>()
                .AddSingleton<FileManager<BotConfiguration>, BotConfigurationFileManager>();
        }
    }
}
