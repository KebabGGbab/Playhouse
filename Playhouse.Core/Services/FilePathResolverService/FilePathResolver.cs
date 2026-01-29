using Microsoft.Extensions.Options;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using System.Globalization;
using System.Reflection;

namespace Playhouse.Core.Services.FilePathResolverService
{
    public class FilePathResolver : IFilePathResolver
	{
		public static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playhouse");
		public static readonly string AppSettings = Path.Combine(AppData, "appsettings.json");
		public static readonly string UserSettings = Path.Combine(AppData, "usersettings.json");

		private FileLocationsOptions _options;

		public FilePathResolver(IOptionsMonitor<FileLocationsOptions> optionsMonitor)
		{
			optionsMonitor.OnChange(options => _options = options);
			_options = optionsMonitor.CurrentValue;
		}

		public string GetPath(FileType fileType)
		{
			return fileType switch
			{
				FileType.DirectoryProfiles => _options.Profiles,
				FileType.DirectoryBots => _options.Bots,
				FileType.FileJSEventsScripts => throw new NotImplementedException(),
				_ => throw new ArgumentOutOfRangeException(nameof(fileType))
			};
		}

		public string GetPath(FileType fileType, int id)
		{
			CultureInfo culture = CultureInfo.InvariantCulture;

			return fileType switch
			{
				FileType.DirectoryProfile => Path.Combine(GetPath(FileType.DirectoryProfiles), id.ToString(culture)),
				FileType.DirectoryUserDataDir => Path.Combine(GetPath(FileType.DirectoryProfiles), id.ToString(culture), "UserDataDir"),
				FileType.FileProfileInfo => Path.Combine(GetPath(FileType.DirectoryProfiles), id.ToString(culture), "ProfileInfo.json"),
				FileType.DirectoryBot => Path.Combine(GetPath(FileType.DirectoryBots), id.ToString(culture)),
				FileType.FileBotInfo => Path.Combine(GetPath(FileType.DirectoryBots), id.ToString(culture), "BotInfo.json"),
				FileType.FileBotDll => Path.Combine(GetPath(FileType.DirectoryBots), id.ToString(culture), "Main.dll"),
				_ => throw new ArgumentOutOfRangeException(nameof(fileType))
			};
		}
	}
}