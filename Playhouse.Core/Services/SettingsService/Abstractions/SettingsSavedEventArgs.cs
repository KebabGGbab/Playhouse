namespace Playhouse.Core.Services.SettingsService.Abstractions
{
    public class SettingsSavedEventArgs<T> : EventArgs
        where T : notnull
    {
        public T NewSettings { get; set; }

        public SettingsSavedEventArgs(T newsSettings) 
        {
            ArgumentNullException.ThrowIfNull(newsSettings, nameof(newsSettings));

            NewSettings = newsSettings;
        }
    }
}
