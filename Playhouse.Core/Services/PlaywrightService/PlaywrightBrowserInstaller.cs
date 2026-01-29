using KebabGGbab.Primitives.Extensions;
using Microsoft.Extensions.Options;
using Playhouse.Core.Enums;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.PlaywrightService
{
    public class PlaywrightBrowserInstaller : IPlaywrightBrowserInstaller
	{
		private PlaywrightOptions _options;

		public PlaywrightBrowserInstaller(IOptionsMonitor<PlaywrightOptions> optionsMonitor)
		{
			_options = optionsMonitor.CurrentValue;
			optionsMonitor.OnChange(options => _options = options);
		}

		public void Install()
		{
			string[] request = GetRequestString(_options);

			int exitCode = Microsoft.Playwright.Program.Main(request);
		}

		private static string[] GetRequestString(PlaywrightOptions options)
		{
			List<string> request = ["install"];

			foreach (string browserType in Enum.GetNames<BrowserType>())
			{
				BrowserType type = Enum.Parse<BrowserType>(browserType);

				if (options.BrowserTypes == type)
				{
					request.Add(GetDisplayEnum(type).ToLowerInvariant());
				}
			}

			return [.. request];
		}

		private static string GetDisplayEnum(Enum field)
		{
			return field.GetDisplayNameField() ?? string.Empty;
		}
	}
}
