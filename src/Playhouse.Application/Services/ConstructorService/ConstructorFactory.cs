using Playhouse.Application.Services.ConstructorService.Abstractions;
using Playhouse.Application.Services.FilePathResolverService.Abstractions;
using Playhouse.Application.Services.PlaywrightService.Abstractions;
using Playhouse.Domain;

namespace Playhouse.Application.Services.ConstructorService
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
