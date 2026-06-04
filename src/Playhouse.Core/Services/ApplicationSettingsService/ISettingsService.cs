using System.Globalization;
using Playhouse.Core.Models;

namespace Playhouse.Core.Services.ApplicationSettingsService
{
    public interface ISettingsService
    {
        CultureInfo CurrentUICulture { get; }

        string PathToData { get; }

        public IReadOnlySet<BrowserTypes> Browsers { get; }

        public IReadOnlySet<BrowserChannels> Channels { get; }

        event EventHandler<ISettingsService, EventArgs>? SettingsChanged;

        Task LoadAsync();

        Task SaveAsync(CultureInfo cultureUI, string pathToData, IEnumerable<BrowserType> browsers, IEnumerable<BrowserChannels> channels);
    }
}
