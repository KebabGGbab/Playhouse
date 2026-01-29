using Playhouse.Core.Models.ConfigurationOptions;

namespace Playhouse.Core.Services.ConfigurationService.Abstractions
{
	public interface IConfigurationUpdater
	{
		event EventHandler ConfigurationSaved;
		Task UpdateAsync(Func<UserSettings, UserSettings> changeConfig);
	}
}
