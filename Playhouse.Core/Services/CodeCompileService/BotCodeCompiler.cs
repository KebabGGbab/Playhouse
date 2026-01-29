using Playhouse.Core.Models;
using Playhouse.Core.Services.CodeCompileService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.CodeCompileService
{
    public sealed class BotCodeCompiler : ICodeCompiler<BotInfo>
    {
        private readonly IFilePathResolver _filePaths;
        private readonly ICompiler _compiler;

        public BotCodeCompiler(IFilePathResolver filePaths, ICompiler compiler)
        {
            _filePaths = filePaths;
            _compiler = compiler;
        }

        public bool Compile(BotInfo bot)
        {
            BotCodeGenerator generator = new(bot);
            CompilationInfo info = new()
            {
                Path = _filePaths.GetPath(FileType.FileBotDll, bot.Id),
                Trees = generator.Generate()
            };

            return _compiler.Compile(info);
        }
    }
}
