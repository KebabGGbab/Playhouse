using Microsoft.Playwright;
using Playhouse.Core.Models;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;
using BrowserType = Playhouse.Core.Enums.BrowserType;

namespace Playhouse.Core.Services.PlaywrightService
{
    public class PlaywrightFactory : IPlaywrightFactory
	{
		private readonly IFilePathResolver _fullPathResolver;

		public PlaywrightFactory(IFilePathResolver fullPath)
		{
			_fullPathResolver = fullPath;
		}

		public async Task<IBrowserContext> CreateBrowserAsync(BrowserProfile profile, BotInfo bot)
		{
			BrowserTypeLaunchPersistentContextOptions options = new()
			{
				//Proxy = profile.ProxyWrapper?.Proxy,
				//ExtraHTTPHeaders = profile.ExtraHTTPHeaderViewModel?.Items.ToDictionary(extraHTTPHeader => extraHTTPHeader.HeaderHttp, extraHTTPHeader => extraHTTPHeader.ValueHttp),
				//HttpCredentials = profile.HttpCredentialsWrapper?.HttpCredentials,
				//IgnoreHTTPSErrors = profile.IgnoreHTTPSErrors,
				//Offline = profile.Offline,
				//RecordHarContent = profile.RecordHarContent,
				//RecordHarMode = profile.RecordHarMode,
				//RecordHarOmitContent = profile.RecordHarOmitContent,
				//RecordHarPath = profile.RecordHarPath,
				////RecordHarUrlFilter
				//ServiceWorkers = profile.ServiceWorkers,
				//Timeout = profile.Timeout,
				Headless = profile.Headless,
				//Geolocation = profile.GeolocationWrapper?.Geolocation,
				//Locale = profile.Locale,
				//TimezoneId = profile.TimeZone,
				//ColorScheme = profile.MediaColorScheme,
				//ForcedColors = profile.MediaForcedColors,
				//ReducedMotion = profile.MediaReducedMotion,
				//AcceptDownloads = profile.AcceptDownloads,
				//BypassCSP = profile.BypassCSP,
				//ChromiumSandbox = profile.ChromiumSandbox,
				//Devtools = profile.DevTools,
				//DeviceScaleFactor = (float?)profile.DeviceScaleFactor,
				//UserAgent = profile.UserAgent,
				//Args = profile.ArgLoadBrowserViewModel?.Items.Select(argLoaBrowser => argLoaBrowser.ToString()),
				//BaseURL = profile.BaseURL,
				Channel = bot.Browser == BrowserType.Chromium ? profile.Channel : null,
				//DownloadsPath = profile.DownloadsPath,
				//ExecutablePath = profile.ExecutablePath,
				//Permissions = profile.Permissions
				SlowMo = profile.SlowMo,
			};

			IPlaywright playwright = await Playwright.CreateAsync().ConfigureAwait(false);
			return bot.Browser switch
			{
                BrowserType.Chromium => await playwright.Chromium.LaunchPersistentContextAsync(_fullPathResolver.GetPath(FileType.DirectoryUserDataDir, profile.Id), options).ConfigureAwait(false),
                BrowserType.Firefox => await playwright.Firefox.LaunchPersistentContextAsync(_fullPathResolver.GetPath(FileType.DirectoryUserDataDir, profile.Id), options).ConfigureAwait(false),
                BrowserType.WebKit => await playwright.Webkit.LaunchPersistentContextAsync(_fullPathResolver.GetPath(FileType.DirectoryUserDataDir, profile.Id), options).ConfigureAwait(false),
				_ => throw new NotSupportedException("Не поддерживаемый тип браузера.")
			};
		}
	}
}
