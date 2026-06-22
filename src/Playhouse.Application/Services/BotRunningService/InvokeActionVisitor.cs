using Microsoft.Playwright;
using Playhouse.Application.Services.VariableService;
using Playhouse.Domain;
using Playhouse.Domain.BotActions;
using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.Application.Services.BotRunningService
{
    public sealed class InvokeActionVisitor : IBotActionAsyncVisitor
    {
        private readonly IBrowserContext _browserContext;
        private readonly BrowserConfiguration _browserConfiguration;


        public InvokeActionVisitor(IBrowserContext browserContext, BrowserConfiguration browserConfiguration)
        {
            ArgumentNullException.ThrowIfNull(browserContext);
            ArgumentNullException.ThrowIfNull(browserConfiguration);

            _browserContext = browserContext;
            _browserConfiguration = browserConfiguration;
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
                .GotoAsync(VariableFormatter.Format(action.Url, _browserConfiguration.UserVariables), (PageGotoOptions)action.Options)
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
                .FillAsync(VariableFormatter.Format(action.Value, _browserConfiguration.UserVariables), (LocatorFillOptions)action.Options)
                .ConfigureAwait(false);
        }
    }
}
