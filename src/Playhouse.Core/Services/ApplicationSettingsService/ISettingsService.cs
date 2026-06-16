using System.Globalization;
using Playhouse.Domain;

namespace Playhouse.Core.Services.ApplicationSettingsService
{
    public interface ISettingsService
    {
        CultureInfo CurrentUICulture { get; }

        string PathToData { get; }

        public IReadOnlySet<BrowserTypes> Browsers { get; }

        public IReadOnlySet<BrowserChannels> Channels { get; }

        event EventHandler<ISettingsService, EventArgs>? SettingsChanged;

        Task SaveAsync(CultureInfo cultureUI, string pathToData, IEnumerable<BrowserTypes> browsers, IEnumerable<BrowserChannels> channels);
    }
}
