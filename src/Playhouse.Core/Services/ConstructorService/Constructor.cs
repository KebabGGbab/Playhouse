using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Resources.Strings;
using Playhouse.Core.Services.ConstructorService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.ConstructorService
{
    public sealed class Constructor : IConstructor, IAsyncDisposable
    {
        private readonly IPlaywrightFactory _playwrightFactory;
        private readonly IFilePathResolver _filePathResolver;

        private readonly ConstructorContext _context = new();

        private IBrowserContext? _browserContext;
        private bool _disposed;

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

        public async Task StartConstructionAsync()
        {
            if (_disposed)
            {
                throw new InvalidOperationException(ExceptionMessages.Constructor_RunCompletedConstruction);
            }
            if (_browserContext is not null)
            {
                throw new InvalidOperationException(ExceptionMessages.Constructor_RunRunningConstructor);
            }

            _browserContext = await _playwrightFactory.CreateBrowserAsync(Profile, Bot).ConfigureAwait(false);
            OnActionHappend(new BrowserContextCreatedBotAction(Bot) 
            { 
                ActionNumber = Bot.Actions.Count,
                Number = _context.GetBrowserContextNumber(_browserContext) 
            });
            await _browserContext.AddInitScriptAsync(scriptPath: _filePathResolver.FileJSEventScripts.FullName).ConfigureAwait(false);
            await _browserContext.ExposeBindingAsync<LocatorActionDataDto>(nameof(SendAction), SendAction).ConfigureAwait(false);
            _browserContext.Close += BrowserClosed;
            _browserContext.Page += PageCreated;
            _context.GetPageNumber(_browserContext.Pages[0]);
        }

        public async Task CompleteConstructionAsync()
        {
            if (_disposed)
            {
                throw new InvalidOperationException(ExceptionMessages.Constructor_StopCompletedConstruction);
            }
            if (_browserContext is null)
            {
                throw new InvalidOperationException(ExceptionMessages.Constructor_StopUnrunningConstruction);
            }

            await _browserContext.CloseAsync()
                .ConfigureAwait(false);
        }

        private void BrowserClosed(object? sender, IBrowserContext e)
        {
            OnActionHappend(new BrowserContextClosedBotAction(Bot)
            {
                ActionNumber = Bot.Actions.Count,
                Number = _context.GetBrowserContextNumber(e) 
            });
            e.Close -= BrowserClosed;
            e.Page -= PageCreated;
            OnConstructionCompleted();
        }

        private void PageCreated(object? sender, IPage e)
        {
            OnActionHappend(new PageCreatedBotAction(Bot) 
            {
                ActionNumber = Bot.Actions.Count,
                Number = _context.GetPageNumber(e) 
            });
            e.Load += PageLoaded;
            e.Close += PageClosed;
        }

        private void PageClosed(object? sender, IPage e)
        {
            OnActionHappend(new PageClosedBotAction(Bot) 
            {
                ActionNumber = Bot.Actions.Count,
                Number = _context.GetPageNumber(e) 
            });
            e.Load -= PageLoaded;
            e.Close -= PageClosed;
        }

        private void PageLoaded(object? sender, IPage e)
        {
            OnActionHappend(new PageGoToBotAction(Bot, e.Url) 
            {
                ActionNumber = Bot.Actions.Count,
                Number = _context.GetPageNumber(e) 
            });
        }

        private void SendAction(BindingSource source, LocatorActionDataDto data)
        {
            BotAction action = data.Action switch
            {
                LocatorClickBotAction.NAME => new LocatorClickBotAction(Bot, data.ToLocatorActionData())
                {
                    ActionNumber = Bot.Actions.Count,
                    Number = _context.GetPageNumber(source.Page!)
                },
                LocatorFillBotAction.NAME => new LocatorFillBotAction(data.Value!, Bot, data.ToLocatorActionData())
                {
                    ActionNumber = Bot.Actions.Count,
                    Number = _context.GetPageNumber(source.Page!)
                },
                _ => throw new NotSupportedException(ExceptionMessages.Constructor_NotSupportedAction)
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

        public async ValueTask DisposeAsync()
        {
            if (_disposed || _browserContext is null)
            {
                return;
            }

            _disposed = true;

            await _browserContext.DisposeAsync()
                .ConfigureAwait(false);

            GC.SuppressFinalize(this);
        }

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