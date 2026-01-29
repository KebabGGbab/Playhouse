using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using KebabGGbab.Json;
using KebabGGbab.TextKit;
using Microsoft.Extensions.Options;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.ConfigurationService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService;

namespace Playhouse.Core.Services.ConfigurationService
{
	public sealed class ConfigurationUpdater : IConfigurationUpdater
	{
		private readonly FileStreamOptions _fileStreamOptions;
		private readonly JsonSerializerOptions _jsonSerializerOptions;

		public UserSettings UserSettings { get; private set; }

		public event EventHandler? ConfigurationSaved;
		 
		public ConfigurationUpdater(IOptionsMonitor<UserSettings> userSettingsOptions)
		{
			UserSettings = userSettingsOptions.CurrentValue;
			userSettingsOptions.OnChange(s => UserSettings = s);

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

		public async Task UpdateAsync(Func<UserSettings, UserSettings> changeConfig)
		{
            UserSettings settings = changeConfig(UserSettings);

			await JsonOperations.WriteJsonAsync(FilePathResolver.UserSettings, settings, _fileStreamOptions, _jsonSerializerOptions).ConfigureAwait(false);

            OnConfigurationSaved();
		}

		private void OnConfigurationSaved()
		{
			ConfigurationSaved?.Invoke(this, EventArgs.Empty);
		}
	}
}
