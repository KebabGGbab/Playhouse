using Microsoft.Playwright;
using Playhouse.Application.Services.PlaywrightService.Abstractions;
using Playhouse.Domain;

namespace Playhouse.Application.Services.BotRunningService
{
    public sealed class RunService : IRunService
    {
        private readonly IPlaywrightFactory _playwrightFactory;
        private readonly List<RunBotUnit> _units;

        private int _progress;

        public BotConfiguration Bot { get; }

        public IReadOnlyList<IRunUnit> Units => _units.AsReadOnly();

        public int Progress => _progress;

        public event EventHandler<IRunService, EventArgs>? ProgressChanged;

        public event EventHandler<IRunService, EventArgs>? RunCompleted;

        public RunService(IPlaywrightFactory playwrightFactory, BotConfiguration bot, IEnumerable<BrowserConfiguration> browsers)
        {
            ArgumentNullException.ThrowIfNull(playwrightFactory);
            ArgumentNullException.ThrowIfNull(bot);
            ArgumentNullException.ThrowIfNull(browsers);

            _playwrightFactory = playwrightFactory;
            Bot = bot;
            _units = browsers.Select(b => new RunBotUnit(bot, b)).ToList();
        }

        public async Task RunAsync()
        {
            await Parallel.ForAsync(0, Units.Count, async (index, cancellation) =>
            {
                IBrowserContext context = await _playwrightFactory.CreateBrowserAsync(_units[index].Browser, Bot).ConfigureAwait(false);
                await _units[index].RunAsync(context).ConfigureAwait(false);
                IncrementProgress();
            }).ConfigureAwait(false);

            OnRunCompleted();
        }

        private void IncrementProgress()
        {
            Interlocked.Increment(ref _progress);
            OnProgressChanged();
        }

        private void OnProgressChanged() 
        { 
            ProgressChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnRunCompleted()
        {
            RunCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
