using Playhouse.Core.Services.BotRunningService.Abstrtactions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotJobManagerFactory : IBotJobManagerFactory
    {
        private readonly IPlaywrightFactory _playwrightFactory;

        public BotJobManagerFactory(IPlaywrightFactory playwrightFactory)
        {
            _playwrightFactory = playwrightFactory;
        }

        public BotJobManager Create(BotJobContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            return new BotJobManager(
                jobContext: context,
                playwrightFactory: _playwrightFactory
                );
        }
    }
}
