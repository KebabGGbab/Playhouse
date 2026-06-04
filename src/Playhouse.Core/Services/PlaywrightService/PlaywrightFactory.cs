using System.Globalization;
using System.Text;
using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Resources.Localization;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;
namespace Playhouse.Core.Services.PlaywrightService
{
    public class PlaywrightFactory : IPlaywrightFactory
	{
		private readonly static CompositeFormat _unsupportedBrowserFormat = CompositeFormat.Parse(StringsCode.UnsupportedBrowser);

        private readonly IFilePathResolver _fullPathResolver;

		public PlaywrightFactory(IFilePathResolver fullPathResolver)
		{
			ArgumentNullException.ThrowIfNull(fullPathResolver, nameof(fullPathResolver));

			_fullPathResolver = fullPathResolver;
		}

		public async Task<IBrowserContext> CreateBrowserAsync(BrowserConfiguration profile, BotConfiguration bot)
		{
			ArgumentNullException.ThrowIfNull(profile, nameof(profile));
			ArgumentNullException.ThrowIfNull(bot, nameof(bot));

			IPlaywright playwright = await Playwright.CreateAsync().ConfigureAwait(false);
			string pathToUserDataDir = _fullPathResolver.GetUserDataDir(profile.Id).FullName;
            BrowserTypeLaunchPersistentContextOptions options = (BrowserTypeLaunchPersistentContextOptions)profile.Options;

            return bot.Browser.Name switch
			{
                nameof(BrowserTypes.Chromium) => await playwright.Chromium.
					LaunchPersistentContextAsync(pathToUserDataDir, options)
					.ConfigureAwait(false),
                nameof(BrowserTypes.Firefox) => await playwright.Firefox
					.LaunchPersistentContextAsync(pathToUserDataDir, options)
					.ConfigureAwait(false),
                nameof(BrowserTypes.WebKit) => await playwright.Webkit
					.LaunchPersistentContextAsync(pathToUserDataDir, options)
					.ConfigureAwait(false),
				_ => throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, _unsupportedBrowserFormat, bot.Browser))
			};
		}
	}
}
