using System.Globalization;
using System.Text;
using Jobs.Abstractions;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Resources.Strings;

namespace Playhouse.Core.Services.BotRunningService
{
    public class BotJob : Job
    {
        private static readonly CompositeFormat PassedIncorrectImplementation = CompositeFormat.Parse(ExceptionMessages.BotJob_IncorrectImplementation);

        public BrowserConfiguration Profile { get; }
        public BotConfiguration Bot { get; }

        public BotJob(BrowserConfiguration profile, BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(profile);
            ArgumentNullException.ThrowIfNull(bot);

            Profile = profile;
            Bot = bot;
        }

        protected override async Task RunAsync(RunArgs args, CancellationToken? cancellation = null)
        {
            ArgumentNullException.ThrowIfNull(args);

            if (args is not BotRunArgs runArgs)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, PassedIncorrectImplementation, nameof(BotRunArgs), nameof(RunArgs), args.GetType().Name), nameof(args));
            }

            InvokeActionVisitor visitor = new(runArgs.BrowserContext);

            foreach (BotAction action in Bot.Actions)
            {
                await action.Accept(visitor).ConfigureAwait(false);
            }
        }
    }
}
