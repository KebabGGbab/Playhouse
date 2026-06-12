using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Core.Services.BotRunningService;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class RunUnitServiceViewModel : ObservableObject
    {
        private readonly IRunUnit _runUnit;

        public BotConfigurationViewModel Bot { get; }

        public BrowserConfigurationViewModel Browser { get; }

        public string Status => _runUnit.Status.ToString();

        public RunUnitServiceViewModel(IRunUnit unit, BotConfigurationViewModel bot, BrowserConfigurationViewModel browser)
        {
            ArgumentNullException.ThrowIfNull(unit);
            ArgumentNullException.ThrowIfNull(bot);
            ArgumentNullException.ThrowIfNull(browser);

            _runUnit = unit;
            _runUnit.StatusChanged += (sender, e) => OnPropertyChanged(nameof(Status));
            Bot = bot;
            Browser = browser;
        }
    }
}
