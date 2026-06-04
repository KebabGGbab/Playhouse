using Playhouse.Core.Services.ApplicationSettingsService;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.Core.Services.PlaywrightService
{
    public class PlaywrightBrowserInstaller : IPlaywrightBrowserInstaller, IInitializer
	{
		private readonly ISettingsService _settings;

		public PlaywrightBrowserInstaller(ISettingsService settings)
		{
			ArgumentNullException.ThrowIfNull(settings);

			_settings = settings;
            _settings.SettingsChanged += SettingsChanged;
		}

        private async void SettingsChanged(ISettingsService sender, EventArgs e)
        {
			await InstallAsync().ConfigureAwait(false);
        }

        public async Task InstallAsync()
		{
			string[][] requests = GetRequestStrings();
			List<Task> tasks = new(requests.Length);

			foreach (string[] request in requests)
			{
				tasks.Add(Task.Run(() => Microsoft.Playwright.Program.Main(request)));
			}

			await Task.WhenAll(tasks).ConfigureAwait(false);
		}

		private string[][] GetRequestStrings()
		{
			return _settings
				.Browsers
				.Select(b => new string[] { "install", "--with-deps", b.Name })
				.ToArray();
		}

        public async Task InitializeAsync()
        {
            await InstallAsync().ConfigureAwait(false);
        }
    }
}
