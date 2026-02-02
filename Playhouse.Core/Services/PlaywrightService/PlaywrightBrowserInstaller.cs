using KebabGGbab.Primitives.Extensions;
using Microsoft.Extensions.Options;
using Playhouse.Core.Enums;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;
using Humanizer;
using Playhouse.Core.Resources;

namespace Playhouse.Core.Services.PlaywrightService
{
    public class PlaywrightBrowserInstaller : IPlaywrightBrowserInstaller, IDisposable
	{
        private readonly IDisposable? _onConfigChangeToken;
		private bool _disposed;
		private PlaywrightOptions _currentConfig;

		public PlaywrightBrowserInstaller(IOptionsMonitor<PlaywrightOptions> config)
		{
			ArgumentNullException.ThrowIfNull(config, nameof(config));

			_currentConfig = config.CurrentValue;
			_onConfigChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
		}

		public async Task Install()
		{
			string[] request = GetRequestString(_currentConfig);

			int exitCode = await Task.Run(() => Microsoft.Playwright.Program.Main(request)).ConfigureAwait(false);

			if (exitCode != 0)
			{
				throw new InvalidOperationException(StringsCode.UnableInstallBrowsers);
			}
		}

		private static string[] GetRequestString(PlaywrightOptions options)
		{
			List<string> request = ["install"];

			foreach (BrowserType browserType in Enum.GetValues<BrowserType>())
			{
				if (browserType == BrowserType.None) continue;

				if (options.BrowserTypes.HasFlag(browserType))
				{
					request.Add(browserType.Humanize());
				}
			}

			return [.. request];
		}

        public void Dispose()
        {
			Dispose(true);
			GC.SuppressFinalize(this);
        }

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			_disposed = true;

			if (disposing)
			{
				_onConfigChangeToken?.Dispose();
			}
		}

		~PlaywrightBrowserInstaller()
		{
			Dispose(false);
		}
    }
}
