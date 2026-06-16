using Playhouse.Core.Services.PlaywrightService.Abstractions;
using Playhouse.Domain;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class RunServiceFactory : IRunServiceFactory
    {
        private readonly IPlaywrightFactory _playwrightFactory;

        public RunServiceFactory(IPlaywrightFactory playwrightFactory)
        {
            ArgumentNullException.ThrowIfNull(playwrightFactory);

            _playwrightFactory = playwrightFactory;
        }

        public IRunService Create(BotConfiguration bot, IEnumerable<BrowserConfiguration> browsers)
        {
            return new RunService(_playwrightFactory, bot, browsers);
        }
    }
}
