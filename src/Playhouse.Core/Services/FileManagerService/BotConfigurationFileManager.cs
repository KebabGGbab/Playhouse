using Playhouse.Core.Models;
using Playhouse.Core.Services.CodeCompileService.Abstractions;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.FileManagerService
{
    public class BotConfigurationFileManager : FileManager<BotConfiguration>
    {
        private readonly ICodeCompiler<BotConfiguration> _compiler;

        public BotConfigurationFileManager(IFilePathResolver pathResolver, ICodeCompiler<BotConfiguration> compiler) : base(pathResolver)
        {
            ArgumentNullException.ThrowIfNull(compiler);

            _compiler = compiler;
        }

        public override void Create(BotConfiguration model)
        {
            ArgumentNullException.ThrowIfNull(model);

            PathResolver.GetBotDirectory(model.Id).Create();
            _compiler.Compile(model);
        }

        public override void Delete(BotConfiguration model)
        {
            ArgumentNullException.ThrowIfNull(model);

            PathResolver.GetBotDirectory(model.Id).Delete(true);
        }
    }
}
