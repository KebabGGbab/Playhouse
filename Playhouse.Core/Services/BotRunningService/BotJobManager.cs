using System.Globalization;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Jobs.Abstractions;
using Microsoft.Playwright;
using PlayhouseShare;
using Playhouse.Core.Models;
using Playhouse.Core.Resources;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotJobManager : JobManager<BotJob>
    {
        private static readonly CompositeFormat _passedIncorrectImplementation = CompositeFormat.Parse(StringsCode.PassedIncorrectImplementation);

        private readonly IPlaywrightFactory _playwrightFactory;
        private readonly AssemblyLoadContext _context;

        public BotInfo Bot { get; init; }

        public BotJobManager(BotJobContext jobContext, IPlaywrightFactory playwrightFactory) 
            : base(jobContext.Profiles.Select(p => new BotJob(p, jobContext.BotInfo)).ToList(), new JobManagerOptions() { Clear = false })
        {
            ArgumentNullException.ThrowIfNull(jobContext, nameof(jobContext));
            ArgumentNullException.ThrowIfNull(playwrightFactory, nameof(playwrightFactory));

            _playwrightFactory = playwrightFactory;
            _context = new AssemblyLoadContext(Assembly.GetExecutingAssembly().Location, true);
            Bot = jobContext.BotInfo;
        }

        public override async Task RunJobsAsync(RunArgs args, CancellationToken? cancellation = null)
        {
            if (args is not BotManagerRunArgs runArgs)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, _passedIncorrectImplementation, nameof(BotManagerRunArgs), nameof(RunArgs), args.GetType().Name), nameof(args));
            }

            Assembly botAssembly = _context.LoadFromAssemblyPath(runArgs.PathToBot);
            Type botInfo = botAssembly.GetTypes().First(t => typeof(Bot).IsAssignableFrom(t));
            Bot bot = (Bot)Activator.CreateInstance(botInfo)!;

            IEnumerable<Task> tasks = Jobs.Select(job =>
            {
                return Task.Run(async () =>
                {
                    IBrowserContext browserContext = await _playwrightFactory.CreateBrowserAsync(job.BrowserProfile, job.BotInfo).ConfigureAwait(false);
                    BotRunArgs argsRunJob = new(bot, browserContext);
                    await job.ExecuteAsync(argsRunJob).ConfigureAwait(false);
                    await browserContext.CloseAsync().ConfigureAwait(false);
                });
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);

            _context.Unload();
        }
    }
}
