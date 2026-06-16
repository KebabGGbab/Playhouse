using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Playhouse.Core.Services.ApplicationSettingsService;
using Playhouse.Core.Services.LocalizationService;
using Playhouse.Domain;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class SettingsViewModel : EditableViewModel
    {
        private readonly ISettingsService _settings;
        private readonly ILocalizator _localizator;

        public IReadOnlyCollection<CultureInfo> Cultures => _localizator.SupportedUICultures; 

        public CultureInfo SelectedCulture 
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public string PathToData
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public IReadOnlyCollection<BrowserTypesViewModel> Browsers { get; }

		public SettingsViewModel(ISettingsService settings, ILocalizator localizator) 
        {
            ArgumentNullException.ThrowIfNull(settings);
            ArgumentNullException.ThrowIfNull(localizator);

            _ = BrowserChannels.Chrome;
            _settings = settings;
            _localizator = localizator;
            Browsers = BrowserTypes.List.Select(b => new BrowserTypesViewModel(b)).ToList().AsReadOnly();
            foreach (BrowserTypesViewModel browser in Browsers)
            {
                browser.IsSelected = _settings.Browsers.Contains(browser.Browser);
                browser.PropertyChanged += Browser_PropertyChanged;

                foreach (BrowserChannelsViewModel channel in browser.Channels)
                {
                    channel.IsSelected = _settings.Channels.Contains(channel.Channel);
                    channel.PropertyChanged += Channel_PropertyChanged;
                }
            }
            CancelChangesCore();
        }

        private void Browser_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            IsModified = CheckModified();
        }
        private void Channel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            IsModified = CheckModified();
        }

        protected override async Task SaveChangesCoreAsync()
        {
            await _settings.SaveAsync(
                cultureUI: SelectedCulture,
                pathToData: PathToData, 
                browsers: Browsers.Where(b => b.IsSelected).Select(b => b.Browser),
                channels: Browsers.Where(b => b.IsSelected).SelectMany(b => b.Channels.Where(c => c.IsSelected)).Select(c => c.Channel));
        }

        [MemberNotNull(nameof(SelectedCulture), nameof(PathToData))]
        protected override void CancelChangesCore()
        {
            SelectedCulture = _settings.CurrentUICulture;
            PathToData = _settings.PathToData;
            foreach (BrowserTypesViewModel browser in Browsers)
            {
                browser.IsSelected = _settings.Browsers.Contains(browser.Browser);

                foreach (BrowserChannelsViewModel channel in browser.Channels)
                {
                    channel.IsSelected = _settings.Channels.Contains(channel.Channel);
                }
            }
        }

        protected override bool CheckModified()
        {
            return !(SelectedCulture == _settings.CurrentUICulture
                && PathToData == _settings.PathToData
                && Browsers.Where(b => b.IsSelected).Select(b => b.Browser).SequenceEqual(_settings.Browsers)
                && Browsers.Where(b => b.IsSelected).SelectMany(b => b.Channels.Where(c => c.IsSelected)).Select(c => c.Channel).SequenceEqual(_settings.Channels));
        }
	}
}
