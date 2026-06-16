using Microsoft.Playwright;
using Playhouse.Domain.BotActions;
using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.Application.Services.BotRunningService
{
    public sealed class InvokeActionVisitor : IBotActionAsyncVisitor
    {
        private readonly IBrowserContext _browserContext;

        public InvokeActionVisitor(IBrowserContext browserContext)
        {
            ArgumentNullException.ThrowIfNull(browserContext);

            _browserContext = browserContext;
        }

        public Task VisitAsync(BrowserContextCreatedBotAction action)
        {
            return Task.CompletedTask;
        }

        public async Task VisitAsync(BrowserContextClosedBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            await _browserContext.CloseAsync((BrowserContextCloseOptions)action.Options)
                .ConfigureAwait(false); 
        }

        public async Task VisitAsync(PageCreatedBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            await _browserContext.NewPageAsync()
                .ConfigureAwait(false);
        }

        public async Task VisitAsync(PageClosedBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            await _browserContext.Pages[action.Number]
                .CloseAsync((PageCloseOptions)action.Options)
                .ConfigureAwait(false);
        }

        public async Task VisitAsync(PageGoToBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            await _browserContext.Pages[action.Number]
                .GotoAsync(action.Url, (PageGotoOptions)action.Options)
                .ConfigureAwait(false);
        }

        public async Task VisitAsync(LocatorClickBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            await action.LocatorData
                .GetLocator(_browserContext.Pages[action.Number])
                .ClickAsync((LocatorClickOptions)action.Options)
                .ConfigureAwait(false);
        }

        public async Task VisitAsync(LocatorFillBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            await action.LocatorData
                .GetLocator(_browserContext.Pages[action.Number])
                .FillAsync(action.Value, (LocatorFillOptions)action.Options)
                .ConfigureAwait(false);
        }
    }
}
