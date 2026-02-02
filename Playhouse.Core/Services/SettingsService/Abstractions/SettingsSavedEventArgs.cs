namespace Playhouse.Core.Services.SettingsService.Abstractions
{
    public class SettingsSavedEventArgs<T> : EventArgs
    {
        public T NewSettings { get; set; }

        public SettingsSavedEventArgs(T newsSettings) 
        {
            NewSettings = newsSettings;
        }
    }
}
