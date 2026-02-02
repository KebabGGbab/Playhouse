using Microsoft.Extensions.Options;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using System.Globalization;

namespace Playhouse.Core.Services.FilePathResolverService
{
    public class FilePathResolver : IFilePathResolver, IDisposable
	{
        private readonly IDisposable? _onConfigChangeToken;
		private bool _disposed;
        private FileLocationsOptions _currentConfig;

        public static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playhouse");
		public static readonly string AppSettings = Path.Combine(AppData, "appsettings.json");
		public static readonly string UserSettings = Path.Combine(AppData, "usersettings.json");

		public string DirectoryProfiles => _currentConfig.Profiles;

		public string DirectoryBots => _currentConfig.Bots;

		public string FileJSEventScripts => throw new NotImplementedException();


        public FilePathResolver(IOptionsMonitor<FileLocationsOptions> config)
		{
			ArgumentNullException.ThrowIfNull(config, nameof(config));

			_currentConfig = config.CurrentValue;
			_onConfigChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
		}

		public string GetPathToDirectoryProfile(int id) => Path.Combine(DirectoryProfiles, id.ToString(CultureInfo.InvariantCulture));

		public string GetPathToDirectoryUserDataDirProfile(int id) => Path.Combine(GetPathToDirectoryProfile(id), "UserDataDir");

		public string GetPathToDirectoryBot(int id) => Path.Combine(DirectoryBots, id.ToString(CultureInfo.InvariantCulture));

		public string GetPathToFileDllBot(int id) => Path.Combine(GetPathToDirectoryBot(id), "Main.dll");

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

		~FilePathResolver()
		{
			Dispose(false);
		}
    }
}