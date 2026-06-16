using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Application.Services.BotRunningService;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class RunServiceViewModel : ObservableObject
    {
        private readonly IRunService _runService;

        public BotConfigurationViewModel Bot { get; }

        public IReadOnlyCollection<RunUnitServiceViewModel> Units { get; }

        public int Progress => _runService.Progress;

        public event EventHandler<RunServiceViewModel, EventArgs>? RunCompleted;

        public RunServiceViewModel(IRunService runService, BotConfigurationViewModel bot, IEnumerable<BrowserConfigurationViewModel> browsers)
        {
            ArgumentNullException.ThrowIfNull(runService);

            _runService = runService;
            _runService.ProgressChanged += RunServiceProgressChanged;
            _runService.RunCompleted += OnRunServiceCompleted;
            Bot = bot;
            Units = browsers.Select(b => new RunUnitServiceViewModel(runService.Units.First(u => u.Browser == b.Profile), bot, b)).ToList().AsReadOnly();
        }

        public async Task RunAsync()
        {
            await _runService.RunAsync();
        }

        private void RunServiceProgressChanged(IRunService sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Progress));
        }

        private void OnRunServiceCompleted(IRunService sender, EventArgs e)
        {
            RunCompleted?.Invoke(this, EventArgs.Empty);
            _runService.ProgressChanged -= RunServiceProgressChanged;
            _runService.RunCompleted -= OnRunServiceCompleted;
        }
    }
}
