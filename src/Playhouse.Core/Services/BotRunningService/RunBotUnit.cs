using Microsoft.Playwright;
using Playhouse.Domain;
using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.Core.Services.BotRunningService
{
    public class RunBotUnit : IRunUnit
    {
        public BotConfiguration Bot { get; }

        public BrowserConfiguration Browser { get; }

        public UnitStatus Status { get; private set; }

        public event EventHandler<IRunUnit, EventArgs>? StatusChanged;

        public RunBotUnit(BotConfiguration bot, BrowserConfiguration browser)
        {
            ArgumentNullException.ThrowIfNull(bot);
            ArgumentNullException.ThrowIfNull(browser);

            Bot = bot;
            Browser = browser;
            Status = UnitStatus.Waiting;
        }

        public async Task RunAsync(IBrowserContext browserContext)
        {
            ArgumentNullException.ThrowIfNull(browserContext);

            InvokeActionVisitor visitor = new(browserContext);

            try
            {
                ChangeStatus(UnitStatus.Running);

                foreach (BotAction action in Bot.Actions)
                {
                    await action.Accept(visitor).ConfigureAwait(false);
                }
                ChangeStatus(UnitStatus.Success);
            }
            catch
            {
                ChangeStatus(UnitStatus.Failed);
            }
        }

        private void ChangeStatus(UnitStatus status)
        {
            Status = status;
            OnStatusChanged();
        }

        private void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
