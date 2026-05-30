using System.Globalization;
using System.Text;
using Jobs.Abstractions;
using Playhouse.Core.Models;
using Playhouse.Core.Resources.Localization;

namespace Playhouse.Core.Services.BotRunningService
{
    public class BotJob : Job
    {
        private static readonly CompositeFormat PassedIncorrectImplementation = CompositeFormat.Parse(StringsCode.PassedIncorrectImplementation);

        public BrowserProfile BrowserProfile { get; init; }
        public BotInfo BotInfo { get; init; }

        public BotJob(BrowserProfile browserProfile, BotInfo botInfo)
        {
            ArgumentNullException.ThrowIfNull(browserProfile, nameof(browserProfile));
            ArgumentNullException.ThrowIfNull(botInfo, nameof(botInfo));

            BrowserProfile = browserProfile;
            BotInfo = botInfo;
        }

        protected override async Task RunAsync(RunArgs args, CancellationToken? cancellation = null)
        {
            ArgumentNullException.ThrowIfNull(args, nameof(args));

            if (args is not BotRunArgs runArgs)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, PassedIncorrectImplementation, nameof(BotRunArgs), nameof(RunArgs), args.GetType().Name), nameof(args));
            }

            await runArgs.Bot.RunAsync(runArgs.BrowserContext, cancellation).ConfigureAwait(false);
        }
    }
}
