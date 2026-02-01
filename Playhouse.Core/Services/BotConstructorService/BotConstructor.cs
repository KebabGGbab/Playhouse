using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.BotConstructorService
{
    public sealed partial class BotConstructor : IBotConstructor
    {
        private readonly IPlaywrightFactory _playwrightFactory;

        public event EventHandler<BrowserEvent>? BrowserEventReceived;
        public event EventHandler? ConstructionCompleted;
        public BotInfo? BotConstruction { get; private set; }

        public BotConstructor(IPlaywrightFactory playwrightFactory)
        {
            _playwrightFactory = playwrightFactory;
        }

        public async Task StartConstructorAsync(BrowserProfile profile, BotInfo bot)
        {
            BotConstruction = bot;
            IBrowserContext browser = await _playwrightFactory.CreateBrowserAsync(profile, bot).ConfigureAwait(false);
            await browser.AddInitScriptAsync(
                """
                document.addEventListener('click', e => {
                    const selector = e.target?.outerHTML?.slice(0, 200);
                    console.log('[Playhouse]:!:click:!::!:' + selector);
                });
                document.addEventListener('input', e => {
                    const selector = e.target?.outerHTML?.slice(0, 200);
                    console.log('[Playhouse]:!:input:!::!:' + selector + ' => ' + e.target.value);
                });
                """).ConfigureAwait(false);

            browser.Console += Console_GetRecord;
            browser.Close += Browser_Closed;
            browser.Page += Page_Created;
        }

        private void Browser_Closed(object? sender, IBrowserContext e)
        {
            e.Console -= Console_GetRecord;
            e.Close -= Browser_Closed;
            e.Page -= Page_Created;
            OnConstructionCompleted();
        }

        private void Page_Created(object? sender, IPage e)
        {
            e.Load += Page_Loaded;
            e.Close += Page_Closed;
        }

        private void Page_Closed(object? sender, IPage e)
        {
            e.Load -= Page_Loaded;
            e.Close -= Page_Closed;
        }

        private void Page_Loaded(object? sender, IPage e)
        {
        }

        private void Console_GetRecord(object? sender, IConsoleMessage e)
        {
            if (!e.Text.StartsWith("[Playhouse]", StringComparison.Ordinal))
            {
                return;
            }

            Match match = ParseConsoleMessage().Match(e.Text);

            BrowserEvent browserEvent = match.Groups["event"].Value switch
            {
                _ => throw new NotSupportedException("Не поддерживаемое действие")
            };

            OnBrowserEventReceived(browserEvent);
        }

        private void OnBrowserEventReceived(BrowserEvent browserEvent)
        {
            BrowserEventReceived?.Invoke(this, browserEvent);
        }

        private void OnConstructionCompleted()
        {
            BotConstruction = null;
            ConstructionCompleted?.Invoke(this, EventArgs.Empty);
        }

        [GeneratedRegex(@"^(?:\[Playhouse\]):!:(?<event>\w{3,20}):!:(?<id>[a-zA-Z0-9_\-.]{0,}):!:(?<text>.{0,})$", RegexOptions.Singleline)]
        private static partial Regex ParseConsoleMessage();
    }
}
