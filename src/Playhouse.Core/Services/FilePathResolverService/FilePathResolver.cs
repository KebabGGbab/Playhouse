using Playhouse.Core.Services.ApplicationSettingsService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using System.Globalization;

namespace Playhouse.Core.Services.FilePathResolverService
{
    public class FilePathResolver : IFilePathResolver
	{
        private readonly ISettingsService _settings;

        public static FileInfo AppSettings { get; } = new FileInfo(Path.Combine(Environment.CurrentDirectory, "appsettings.json"));

        public DirectoryInfo Browsers => new(Path.Combine(_settings.PathToData, nameof(Browsers)));

        public DirectoryInfo Bots => new(Path.Combine(_settings.PathToData, nameof(Bots)));

		public FileInfo FileJSEventScripts { get; }

        public FilePathResolver(ISettingsService settings)
		{
            ArgumentNullException.ThrowIfNull(settings);

            _settings = settings;
			FileJSEventScripts = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Resources", "js", "BrowserEventsListener.js"));
        }

        public DirectoryInfo GetBrowserDirectory(int browserId)
        {
            return new DirectoryInfo(Path.Combine(Browsers.FullName, browserId.ToString(CultureInfo.InvariantCulture)));
        }

        public DirectoryInfo GetUserDataDir(int browserId)
        {
            return new DirectoryInfo(Path.Combine(GetBrowserDirectory(browserId).FullName, "UserDataDir"));
        }

        public DirectoryInfo GetBotDirectory(int botId)
        {
            return new DirectoryInfo(Path.Combine(Bots.FullName, botId.ToString(CultureInfo.InvariantCulture)));
        }

        public FileInfo GetBotDllFile(int botId)
        {
            return new FileInfo(Path.Combine(GetBotDirectory(botId).FullName, "Main.dll"));
        }

    }
}