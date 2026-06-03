using Playhouse.Core.Models;
using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.BotConstructorService
{
    public sealed class BotConstructorFactory : IBotConstructorFactory
    {
        private readonly IPlaywrightFactory _playwrightFactory;
        private readonly IFilePathResolver _filePathResolver;

        public BotConstructorFactory(IPlaywrightFactory playwrightFactory, IFilePathResolver filePathResolver) 
        { 
            _playwrightFactory = playwrightFactory;
            _filePathResolver = filePathResolver;
        }

        public IBotConstructor Create(BrowserConfiguration profile, BotConfiguration bot)
        {
            return new BotConstructor(_playwrightFactory, _filePathResolver, profile, bot);
        }
    }
}
