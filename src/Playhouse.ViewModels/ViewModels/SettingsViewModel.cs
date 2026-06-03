using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Humanizer;
using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using KebabGGbab.Localization.Manager;
using Playhouse.Core.Enums;
using Playhouse.Core.Services.ApplicationSettingsService;
using Playhouse.UI.Services.LocalizationService;
using Playhouse.ViewModels.ViewModels.Abstractions;

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

        public IReadOnlyCollection<SelectableItem<string>> Browsers { get; }

        public IReadOnlyCollection<SelectableItem<string>> Channels { get; }

		public SettingsViewModel(ISettingsService settings, ILocalizator localizator) 
        {
            ArgumentNullException.ThrowIfNull(settings);
            ArgumentNullException.ThrowIfNull(localizator);

            _settings = settings;
            _localizator = localizator;
            Browsers = Enum.GetValues<BrowserType>().Select(b => new SelectableItem<string>(b.Humanize())).ToList().AsReadOnly();
            foreach (SelectableItem<string> browser in Browsers)
            {
                browser.PropertyChanged += Browser_PropertyChanged;
            }
            Channels = Enum.GetValues<BrowserChannels>().Select(c => new SelectableItem<string>(c.Humanize())).ToList().AsReadOnly();
            foreach (SelectableItem<string> channel in Channels)
            {
                channel.PropertyChanged += Channel_PropertyChanged;
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
                SelectedCulture,
                PathToData, 
                Browsers.Where(b => b.IsSelected).Select(b => b.Item.DehumanizeTo<BrowserType>()),
                Channels.Where(b => b.IsSelected).Select(c => c.Item.DehumanizeTo<BrowserChannels>()));
        }

        [MemberNotNull(nameof(SelectedCulture), nameof(PathToData))]
        protected override void CancelChangesCore()
        {
            SelectedCulture = _settings.CurrentUICulture;
            PathToData = _settings.PathToData;
            foreach (SelectableItem<string> browser in Browsers)
            {
                browser.IsSelected = _settings.Browsers.Contains(browser.Item.DehumanizeTo<BrowserType>());
            }
            foreach (SelectableItem<string> channel in Channels)
            {
                channel.IsSelected = _settings.Channels.Contains(channel.Item.DehumanizeTo<BrowserChannels>());
            }
        }

        protected override bool CheckModified()
        {
            return !(SelectedCulture == _settings.CurrentUICulture
                && PathToData == _settings.PathToData
                && Browsers.Where(b => b.IsSelected).Select(b => b.Item.DehumanizeTo<BrowserType>()).SequenceEqual(_settings.Browsers)
                && Channels.Where(b => b.IsSelected).Select(c => c.Item.DehumanizeTo<BrowserChannels>()).SequenceEqual(_settings.Channels));
        }
	}
}
