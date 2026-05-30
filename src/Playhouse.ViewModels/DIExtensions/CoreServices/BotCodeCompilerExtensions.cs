using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Services.CodeCompileService;
using Playhouse.Core.Services.CodeCompileService.Abstractions;
using PlayhouseShare;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class BotCodeCompilerExtensions
    {
        public static IServiceCollection AddBotCompiler(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            string[] assembliesPath = [
                typeof(IPlaywright).Assembly.Location,
                typeof(Bot).Assembly.Location,
                ];

            string[] trusted = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator);

            services.AddSingleton<ICompiler, Compiler>((s) =>
            {
                CSharpCompilationOptions options = new(OutputKind.DynamicallyLinkedLibrary);
                IEnumerable<MetadataReference> references = assembliesPath.Concat(trusted).Select(p => MetadataReference.CreateFromFile(p));

                return new Compiler(options, references);
            });
            services.AddSingleton<ICodeCompiler<BotInfo>, BotCodeCompiler>();

            return services;
        }
    }
}
