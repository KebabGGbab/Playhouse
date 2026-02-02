using System.Globalization;
using System.Text;
using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Resources;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;
using BrowserType = Playhouse.Core.Enums.BrowserType;

namespace Playhouse.Core.Services.PlaywrightService
{
    public class PlaywrightFactory : IPlaywrightFactory
	{
		private readonly static CompositeFormat _unsupportedBrowserFormat = CompositeFormat.Parse(StringsCode.UnsupportedBrowser);

        private readonly IFilePathResolver _fullPathResolver;

		public PlaywrightFactory(IFilePathResolver fullPath)
		{
			_fullPathResolver = fullPath;
		}

		public async Task<IBrowserContext> CreateBrowserAsync(BrowserProfile profile, BotInfo bot)
		{
			ArgumentNullException.ThrowIfNull(profile, nameof(profile));
			ArgumentNullException.ThrowIfNull(bot, nameof(bot));

			IPlaywright playwright = await Playwright.CreateAsync().ConfigureAwait(false);

			return bot.Browser switch
			{
                BrowserType.Chromium => await playwright.Chromium.
					LaunchPersistentContextAsync(_fullPathResolver.GetPath(FileType.DirectoryUserDataDir, profile.Id), profile)
					.ConfigureAwait(false),
                BrowserType.Firefox => await playwright.Firefox
					.LaunchPersistentContextAsync(_fullPathResolver.GetPath(FileType.DirectoryUserDataDir, profile.Id), profile)
					.ConfigureAwait(false),
                BrowserType.WebKit => await playwright.Webkit
					.LaunchPersistentContextAsync(_fullPathResolver.GetPath(FileType.DirectoryUserDataDir, profile.Id), profile)
					.ConfigureAwait(false),
				_ => throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, _unsupportedBrowserFormat, bot.Browser))
			};
		}
	}
}
