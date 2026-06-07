using Jobs.Abstractions;
using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotJobManager : JobManager<BotJob>
    {
        private readonly IPlaywrightFactory _playwrightFactory;

        public BotConfiguration Bot { get; }

        public BotJobManager(BotJobContext jobContext, IPlaywrightFactory playwrightFactory) 
            : base(jobContext.Profiles.Select(p => new BotJob(p, jobContext.Bot)).ToList(), new JobManagerOptions() { Clear = false })
        {
            ArgumentNullException.ThrowIfNull(jobContext);
            ArgumentNullException.ThrowIfNull(playwrightFactory);

            _playwrightFactory = playwrightFactory;
            Bot = jobContext.Bot;
        }

        public override async Task RunJobsAsync(RunArgs args, CancellationToken? cancellation = null)
        {
            IEnumerable<Task> tasks = Jobs.Select(job =>
            {
                return Task.Run(async () =>
                {
                    IBrowserContext browserContext = await _playwrightFactory.CreateBrowserAsync(job.Profile, job.Bot).ConfigureAwait(false);
                    BotRunArgs argsRunJob = new(browserContext);
                    await job.ExecuteAsync(argsRunJob).ConfigureAwait(false);
                });
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
