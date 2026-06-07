using Playhouse.Core.Models;
using Playhouse.Core.Services.ConstructorService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.ConstructorService
{
    public sealed class ConstructorFactory : IConstructorFactory
    {
        private readonly IPlaywrightFactory _playwrightFactory;
        private readonly IFilePathResolver _filePathResolver;

        public ConstructorFactory(IPlaywrightFactory playwrightFactory, IFilePathResolver filePathResolver) 
        {
            ArgumentNullException.ThrowIfNull(playwrightFactory);
            ArgumentNullException.ThrowIfNull(filePathResolver);

            _playwrightFactory = playwrightFactory;
            _filePathResolver = filePathResolver;
        }

        public IConstructor Create(BrowserConfiguration profile, BotConfiguration bot)
        {
            return new Constructor(_playwrightFactory, _filePathResolver, profile, bot);
        }
    }
}
