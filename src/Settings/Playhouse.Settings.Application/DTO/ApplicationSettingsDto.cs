namespace Playhouse.Settings.Application.DTO
{
    public class ApplicationSettingsDto
    {
        public string CultureName { get; }

        public string PathToData { get; }

        public IReadOnlyList<string> Browsers { get; }

        public IReadOnlyList<string> Channels { get; }

        public ApplicationSettingsDto(string cultureName, string pathToData, 
            IEnumerable<string> addedBrowsers, IEnumerable<string> addedChannels)
        {
            CultureName = cultureName;
            PathToData = pathToData;
            Browsers = addedBrowsers.ToList().AsReadOnly();
            Channels = addedChannels.ToList().AsReadOnly();
        }
    }
}
