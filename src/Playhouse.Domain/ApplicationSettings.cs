using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Playhouse.Domain;

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

        public void Update(string cultureCode, string pathToData, IEnumerable<BrowserTypes> browsers, IEnumerable<BrowserChannels> channels)
        {
            Validate(cultureCode, pathToData, browsers, channels);

            HashSet<BrowserTypes> browserTypes = browsers.ToHashSet();
            HashSet<BrowserChannels> browserChannels = channels.ToHashSet();
            _uiCultureCode = cultureCode;
            PathToData = pathToData;
            foreach (BrowserTypes browser in BrowserTypes.List)
            {
                bool isSaved = Browsers.Contains(browser);
                bool isNotSaved = browserTypes.Contains(browser);
                if (isSaved && !isNotSaved)
                {
                    Browsers.Remove(browser);
                }
                else if (!isSaved && isNotSaved)
                {
                    Browsers.Add(browser);
                }
            }
            foreach (BrowserChannels channel in BrowserChannels.List)
            {
                bool isSaved = Channels.Contains(channel);
                bool isNotSaved = browserChannels.Contains(channel);
                if (isSaved && !isNotSaved)
                {
                    Channels.Remove(channel);
                }
                else if (!isSaved && isNotSaved)
                {
                    Channels.Add(channel);
                }
            }
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
