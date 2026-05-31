using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.BotConstructorService
{
    public sealed partial class BotConstructor : IBotConstructor
    {
        private readonly IPlaywrightFactory _playwrightFactory;
        private readonly IFilePathResolver _filePathResolver;

        private readonly ConstructorContext _context = new();

        public BotInfo BotConstruction { get; }

        public BrowserProfile ProfileConstruct { get; }

        public event EventHandler<IBotConstructor, BrowserEventHappenedEventArgs>? BrowserEventHappend;

        public event EventHandler<IBotConstructor, BotConstructionCompletedEventArgs>? ConstructionCompleted;

        public BotConstructor(IPlaywrightFactory playwrightFactory, IFilePathResolver filePathResolver, BrowserProfile profile, BotInfo bot)
        {
            ArgumentNullException.ThrowIfNull(playwrightFactory, nameof(playwrightFactory));
            ArgumentNullException.ThrowIfNull(filePathResolver, nameof(filePathResolver));
            ArgumentNullException.ThrowIfNull(profile, nameof(profile));
            ArgumentNullException.ThrowIfNull(bot, nameof(bot));

            _playwrightFactory = playwrightFactory;
            _filePathResolver = filePathResolver;
            BotConstruction = bot;
            ProfileConstruct = profile;
        }

        public async Task StartConstructorAsync()
        {
            IBrowserContext browser = await _playwrightFactory.CreateBrowserAsync(ProfileConstruct, BotConstruction).ConfigureAwait(false);
            await browser.AddInitScriptAsync(scriptPath: _filePathResolver.FileJSEventScripts).ConfigureAwait(false);

            browser.Console += Console_GetRecord;
            browser.Close += Browser_Closed;
            browser.Page += Page_Created;
        }

        private void Browser_Closed(object? sender, IBrowserContext e)
        {
            OnBrowserEventHappend(new BrowserContextClosedBrowserEvent() { BotInfo = BotConstruction, Number = _context.GetBrowserContextNumber(e) });
            e.Console -= Console_GetRecord;
            e.Close -= Browser_Closed;
            e.Page -= Page_Created;
            OnConstructionCompleted();
        }

        private void Page_Created(object? sender, IPage e)
        {
            OnBrowserEventHappend(new PageCreatedBrowserEvent() { BotInfo = BotConstruction, Number = _context.GetPageNumber(e) });
            e.Load += Page_Loaded;
            e.Close += Page_Closed;
        }

        private void Page_Closed(object? sender, IPage e)
        {
            OnBrowserEventHappend(new PageClosedBrowserEvent() { BotInfo = BotConstruction, Number = _context.GetPageNumber(e) });
            e.Load -= Page_Loaded;
            e.Close -= Page_Closed;
        }

        private void Page_Loaded(object? sender, IPage e)
        {
            OnBrowserEventHappend(new PageGoToBrowserEvent(e.Url) { BotInfo = BotConstruction, Number = _context.GetPageNumber(e) });
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

            OnBrowserEventHappend(browserEvent);
        }

        private void OnBrowserEventHappend(BrowserEvent browserEvent)
        {
            BrowserEventHappend?.Invoke(this, new BrowserEventHappenedEventArgs(browserEvent));
        }

        private void OnConstructionCompleted()
        {
            ConstructionCompleted?.Invoke(this, new BotConstructionCompletedEventArgs(BotConstruction));
        }

        [GeneratedRegex(@"^(?:\[Playhouse\]):!:(?<event>\w{3,20}):!:(?<id>[a-zA-Z0-9_\-.]{0,}):!:(?<text>.{0,})$", RegexOptions.Singleline)]
        private static partial Regex ParseConsoleMessage();

        private class ConstructorContext
        {
            private Dictionary<IPage, int> _pageNumbers = [];
            private Dictionary<IBrowserContext, int> _browserContextNumbers = [];

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