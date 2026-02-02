namespace Playhouse.Core.Services.SettingsService.Abstractions
{
	public interface ISettingsUpdater<T>
	{
		event EventHandler<ISettingsUpdater<T>, SettingsSavedEventArgs<T>> SettingsSaved;
		Task UpdateAsync(T settings);
	}
}
