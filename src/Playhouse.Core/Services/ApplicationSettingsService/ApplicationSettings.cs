using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Playhouse.Core.Models;

namespace Playhouse.Core.Services.ApplicationSettingsService
{
    public class ApplicationSettings
    {
        public int Id { get; private set; }

        private string _uiCultureCode;

        public CultureInfo UICulture 
        {
            get => CultureInfo.GetCultureInfo(_uiCultureCode);
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                _uiCultureCode = value.Name;
            }
        }

        public string PathToData 
        { 
            get; 
            set
            {
                DirectoryInfo directory = new(value);
                field = directory.FullName;
            }
        }

        public ISet<BrowserTypes> Browsers { get; } 

        public ISet<BrowserChannels> Channels { get; }

        public ApplicationSettings()
        {
            _uiCultureCode = CultureInfo.CurrentUICulture.Name;
            PathToData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playhouse");
            Browsers = new HashSet<BrowserTypes>();
            Channels = new HashSet<BrowserChannels>();
        }

        public ApplicationSettings(string cultureCode, string pathToData, IEnumerable<BrowserType> browsers, IEnumerable<BrowserChannels> channels)
        {
            Validate(cultureCode, pathToData, browsers, channels);

            _uiCultureCode = cultureCode;
            PathToData = pathToData;
            Browsers = new HashSet<BrowserType>(browsers);
            Channels = new HashSet<BrowserChannels>(channels);
        }

        private static void Validate([NotNull]string cultureCode, string pathToData, IEnumerable<BrowserTypes> browsers, IEnumerable<BrowserChannels> channels)
        {
            CultureInfo.GetCultureInfo(cultureCode); // В этом методе есть все проверки для культуры
            ArgumentException.ThrowIfNullOrWhiteSpace(pathToData);
            ArgumentNullException.ThrowIfNull(browsers);
            ArgumentNullException.ThrowIfNull(channels);
        }
    }
}
