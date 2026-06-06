using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Services.ConstructorService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.BotConstructorService
{
    public sealed partial class Constructor : IConstructor
    {
        private readonly IPlaywrightFactory _playwrightFactory;
        private readonly IFilePathResolver _filePathResolver;

        private readonly ConstructorContext _context = new();

        public BotConfiguration Bot { get; }

        public BrowserConfiguration Profile { get; }

        public event EventHandler<IConstructor, BrowserEventHappenedEventArgs>? ActionHappend;

        public event EventHandler<IConstructor, ConstructionCompletedEventArgs>? ConstructionCompleted;

        public Constructor(IPlaywrightFactory playwrightFactory, IFilePathResolver filePathResolver, BrowserConfiguration profile, BotConfiguration bot)
        {
            ArgumentNullException.ThrowIfNull(playwrightFactory);
            ArgumentNullException.ThrowIfNull(filePathResolver);
            ArgumentNullException.ThrowIfNull(profile);
            ArgumentNullException.ThrowIfNull(bot);

            _playwrightFactory = playwrightFactory;
            _filePathResolver = filePathResolver;
            Bot = bot;
            Profile = profile;
        }

        public async Task StartConstructorAsync()
        {
            IBrowserContext browser = await _playwrightFactory.CreateBrowserAsync(Profile, Bot).ConfigureAwait(false);
            await browser.AddInitScriptAsync(scriptPath: _filePathResolver.FileJSEventScripts.FullName).ConfigureAwait(false);

            browser.Console += Console_GetRecord;
            browser.Close += Browser_Closed;
            browser.Page += Page_Created;
        }

        private void Browser_Closed(object? sender, IBrowserContext e)
        {
            OnActionHappend(new BrowserContextClosedBotAction() { Bot = Bot, Number = _context.GetBrowserContextNumber(e) });
            e.Console -= Console_GetRecord;
            e.Close -= Browser_Closed;
            e.Page -= Page_Created;
            OnConstructionCompleted();
        }

        private void Page_Created(object? sender, IPage e)
        {
            OnActionHappend(new PageCreatedBotAction() { Bot = Bot, Number = _context.GetPageNumber(e) });
            e.Load += Page_Loaded;
            e.Close += Page_Closed;
        }

        private void Page_Closed(object? sender, IPage e)
        {
            OnActionHappend(new PageClosedBotAction() { Bot = Bot, Number = _context.GetPageNumber(e) });
            e.Load -= Page_Loaded;
            e.Close -= Page_Closed;
        }

        private void Page_Loaded(object? sender, IPage e)
        {
            OnActionHappend(new PageGoToBotAction(e.Url) { Bot = Bot, Number = _context.GetPageNumber(e) });
        }

        private void Console_GetRecord(object? sender, IConsoleMessage e)
        {
            if (!e.Text.StartsWith("[Playhouse]", StringComparison.Ordinal))
            {
                return;
            }

            Match match = ParseConsoleMessage().Match(e.Text);

            BotAction action = match.Groups["event"].Value switch
            {
                _ => throw new NotSupportedException("Не поддерживаемое действие")
            };

            OnActionHappend(action);
        }

        private void OnActionHappend(BotAction action)
        {
            ActionHappend?.Invoke(this, new BrowserEventHappenedEventArgs(action));
        }

        private void OnConstructionCompleted()
        {
            ConstructionCompleted?.Invoke(this, new ConstructionCompletedEventArgs(Bot));
        }

        [GeneratedRegex(@"^(?:\[Playhouse\]):!:(?<event>\w{3,20}):!:(?<id>[a-zA-Z0-9_\-.]{0,}):!:(?<text>.{0,})$", RegexOptions.Singleline)]
        private static partial Regex ParseConsoleMessage();

        private class ConstructorContext
        {
            private readonly Dictionary<IPage, int> _pageNumbers = [];
            private readonly Dictionary<IBrowserContext, int> _browserContextNumbers = [];

            public int GetPageNumber(IPage page)
            {
                if (!_pageNumbers.TryGetValue(page, out int result))
                {
                    result = _pageNumbers.Count;
                    _pageNumbers.Add(page, result);
                }

                return result;
            }

            public int GetBrowserContextNumber(IBrowserContext browserContext)
            {
                if (!_browserContextNumbers.TryGetValue(browserContext, out int result))
                {
                    result = _browserContextNumbers.Count;
                    _browserContextNumbers.Add(browserContext, result);
                }

                return result;
            }
        }
    }
}