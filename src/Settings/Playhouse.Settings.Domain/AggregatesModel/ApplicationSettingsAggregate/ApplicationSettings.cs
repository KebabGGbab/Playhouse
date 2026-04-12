using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.Resources.Strings;

namespace Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate
{
    public sealed class ApplicationSettings : AggregateRoot
    {
        private readonly HashSet<BrowserType> _browsers;
        private readonly HashSet<BrowserChannel> _channels;

        public Culture Culture { get; private set; }

        public DirectoryPath PathToData { get; private set; }

        public IEnumerable<BrowserType> Browsers => _browsers.AsReadOnly();

        public IEnumerable<BrowserChannel> Channels => _channels.AsReadOnly();

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        private ApplicationSettings()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        {
        }

        private ApplicationSettings(Culture culture, DirectoryPath pathToData, IEnumerable<BrowserType> browsers, IEnumerable<BrowserChannel> channels)
        {
            Culture = culture;
            PathToData = pathToData;
            _browsers = new HashSet<BrowserType>(browsers);
            _channels = new HashSet<BrowserChannel>(channels);
        }

        public static ApplicationSettings Create(Culture? culture = null, DirectoryPath? pathToData = null, 
            IEnumerable<BrowserType>? browsers = null, IEnumerable<BrowserChannel>? channels = null)
        {
            culture ??= Culture.Default;
            pathToData ??= DirectoryPath.Default;
            browsers ??= [];
            channels ??= [];

            return new ApplicationSettings(culture, pathToData, browsers, channels);
        }

        public Result<ApplicationSettings> AddBrowser(BrowserType browser)
        {
            if (browser == null)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsBrowserNotSpecified);
            }

            return _browsers.Add(browser)
                ? Result<ApplicationSettings>.Ok(this)
                : Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsBrowserAlreadyAdded);
        }

        public Result<ApplicationSettings> RemoveBrowser(BrowserType browser)
        {
            if (browser == null)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsBrowserNotSpecified);
            }

            return _browsers.Remove(browser) 
                ? Result<ApplicationSettings>.Ok(this) 
                : Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsBrowserMissing);
        }

        public Result<ApplicationSettings> AddChannel(BrowserChannel channel)
        {
            if (channel == null)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsChannelNotSpecified);
            }

            return _channels.Add(channel)
                ? Result<ApplicationSettings>.Ok(this)
                : Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsChannelAlreadyAdded);
        }

        public Result<ApplicationSettings> RemoveChannel(BrowserChannel channel)
        {
            if (channel == null)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsChannelNotSpecified);
            }

            return _channels.Remove(channel)
                ? Result<ApplicationSettings>.Ok(this)
                : Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsChannelMissing);
        }

        public Result<ApplicationSettings> ChangeCulture(Culture culture)
        {
            if (culture == null)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsCultureNotSpecified);
            }

            if (Culture == culture)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsIdenticalCultureAlreadySet);
            }

            Culture = culture;

            return Result<ApplicationSettings>.Ok(this);
        }

        public Result<ApplicationSettings> ChangePathToData(DirectoryPath pathToData)
        {
            if (pathToData == null)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsDirectoryNotSpecified);
            }

            if (PathToData == pathToData)
            {
                return Result<ApplicationSettings>.Fail(ErrorMessages.ApplicationSettingsIdenticalPathToDataAlreadySet);
            }

            PathToData = pathToData;

            return Result<ApplicationSettings>.Ok(this);
        }
    }
}
