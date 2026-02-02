using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using KebabGGbab.Json;
using KebabGGbab.TextKit;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.SettingsService.Abstractions;

namespace Playhouse.Core.Services.SettingsService
{
	public class UserSettingsUpdater : ISettingsUpdater<UserSettings>
	{
		private readonly FileStreamOptions _fileStreamOptions;
		private readonly JsonSerializerOptions _jsonSerializerOptions;

		public event EventHandler<ISettingsUpdater<UserSettings>, SettingsSavedEventArgs<UserSettings>>? SettingsSaved;
		
		public UserSettingsUpdater()
		{
			_fileStreamOptions = FileStreamOptionsDefaults.Write;
			_jsonSerializerOptions = new JsonSerializerOptions()
			{
				WriteIndented = true,
				Converters =
					{
						new JsonStringEnumConverter()
					},
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
			};
		}

		public async Task UpdateAsync(UserSettings settings)
		{
			await JsonOperations.WriteJsonAsync(FilePathResolver.UserSettings, settings, _fileStreamOptions, _jsonSerializerOptions).ConfigureAwait(false);

            OnConfigurationSaved(settings);
		}

		private void OnConfigurationSaved(UserSettings settings)
		{
            SettingsSaved?.Invoke(this, new SettingsSavedEventArgs<UserSettings>(settings));
		}
	}
}
