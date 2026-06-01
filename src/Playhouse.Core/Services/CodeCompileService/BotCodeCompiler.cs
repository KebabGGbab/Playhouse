using Playhouse.Core.Models;
using Playhouse.Core.Services.CodeCompileService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.CodeCompileService
{
    public class BotCodeCompiler : ICodeCompiler<BotConfiguration>
    {
        private readonly IFilePathResolver _filePathResolver;
        private readonly ICompiler _compiler;

        public BotCodeCompiler(IFilePathResolver filePathResolver, ICompiler compiler)
        {
            ArgumentNullException.ThrowIfNull(filePathResolver, nameof(filePathResolver));
            ArgumentNullException.ThrowIfNull(compiler, nameof(compiler));

            _filePathResolver = filePathResolver;
            _compiler = compiler;
        }

        public bool Compile(BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));

            BotCodeGenerator generator = new(bot);
            CompilationInfo info = new()
            {
                Path = _filePathResolver.GetPathToFileDllBot(bot.Id),
                Trees = generator.Generate()
            };

            return _compiler.Compile(info);
        }
    }
}
