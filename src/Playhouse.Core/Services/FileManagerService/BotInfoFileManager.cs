using Playhouse.Core.Models;
using Playhouse.Core.Services.CodeCompileService.Abstractions;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.FileManagerService
{
    public class BotInfoFileManager : FileManager<BotInfo>
    {
        private readonly ICodeCompiler<BotInfo> _compiler;

        public BotInfoFileManager(IFilePathResolver pathResolver, ICodeCompiler<BotInfo> compiler) : base(pathResolver)
        {
            _compiler = compiler;
        }

        public override void Create(BotInfo model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            Directory.CreateDirectory(PathResolver.GetPathToDirectoryBot(model.Id));
            _compiler.Compile(model);
        }

        public override void Delete(BotInfo model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            Directory.Delete(PathResolver.GetPathToDirectoryBot(model.Id), true);
        }
    }
}
