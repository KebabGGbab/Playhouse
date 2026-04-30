using Playhouse.SharedKernel.Domain.BaseModels;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Channels;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Browsers;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Culture;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.PathToData;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.DomainEvents;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate
{
    public sealed class ApplicationSettings : AggregateRoot
    {
        private readonly CanAddBrowserValidationSpecification _canAddBrowserRule;
        private readonly CanRemoveBrowserValidationSpecification _canRemoveBrowserRule;
        private readonly CanAddChannelValidationSpecification _canAddChannelRule;
        private readonly CanRemoveChannelValidationSpecification _canRemoveChannelRule;
        private readonly CanCultureChangeValidationSpecification _canCultureChangeRule;
        private readonly CanPathToDataChangeValidationSpecification _canPathToDataChangeRule;

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
            _browsers = [.. browsers];
            _channels = [.. channels];
            _canAddBrowserRule = new(_browsers.AsReadOnly());
            _canRemoveBrowserRule = new(_browsers.AsReadOnly());
            _canAddChannelRule = new(_channels.AsReadOnly());
            _canRemoveChannelRule = new(_channels.AsReadOnly());
            _canCultureChangeRule = new(this);
            _canPathToDataChangeRule = new(this);
        }

        public static Result<ApplicationSettings> Create(Culture? culture = null, DirectoryPath? pathToData = null, 
            IEnumerable<BrowserType>? browsers = null, IEnumerable<BrowserChannel>? channels = null)
        {
            culture ??= Culture.Default;
            pathToData ??= DirectoryPath.Default;
            browsers ??= [];
            channels ??= [];

            return Result.Ok(new ApplicationSettings(culture, pathToData, browsers, channels));
        }

        public Result CanAddBrowser(BrowserType browser)
        {
            return _canAddBrowserRule.IsSatisfiedBy(browser);
        }

        public Result AddBrowser(BrowserType browser)
        {
            Result canAdd = CanAddBrowser(browser);

            if (canAdd.IsSuccess)
            {
                _browsers.Add(browser);
                AddDomainEvent(new BrowserAddedDomainEvent(browser));
            }

            return canAdd;
        }

        public Result CanRemoveBrowser(BrowserType browser)
        {
            return _canRemoveBrowserRule.IsSatisfiedBy(browser);
        }

        public Result RemoveBrowser(BrowserType browser)
        {
            Result canRemove = CanRemoveBrowser(browser);

            if (canRemove.IsSuccess)
            {
                _browsers.Remove(browser);
                AddDomainEvent(new BrowserRemovedDomainEvent(browser));
            }

            return canRemove;
        }

        public Result CanAddChannel(BrowserChannel channel)
        {
            return _canAddChannelRule.IsSatisfiedBy(channel);
        }

        public Result AddChannel(BrowserChannel channel)
        {
            Result canAdd = CanAddChannel(channel);

            if (canAdd.IsSuccess)
            {
                _channels.Add(channel);
                AddDomainEvent(new ChannelAddedDomainEvent(channel));
            }

            return canAdd;
        }

        public Result CanRemoveChannel(BrowserChannel channel)
        {
            return _canRemoveChannelRule.IsSatisfiedBy(channel);
        }

        public Result RemoveChannel(BrowserChannel channel)
        {
            Result canRemove = CanRemoveChannel(channel);

            if (canRemove.IsSuccess)
            {
                _channels.Remove(channel);
                AddDomainEvent(new ChannelRemovedDomainEvent(channel));
            }

            return canRemove;
        }

        public Result CanCultureChange(Culture culture)
        {
            return _canCultureChangeRule.IsSatisfiedBy(culture);
        }

        public Result ChangeCulture(Culture culture)
        {
            Result canChange = CanCultureChange(culture);

            if (canChange.IsSuccess)
            {
                Culture oldCulture = Culture;
                Culture = culture;
                AddDomainEvent(new CultureChangedDomainEvent(oldCulture, culture));
            }

            return canChange;
        }

        public Result CanChangePathToData(DirectoryPath pathToData)
        {
            return _canPathToDataChangeRule.IsSatisfiedBy(pathToData);
        }

        public Result ChangePathToData(DirectoryPath pathToData)
        {
            Result canChange = CanChangePathToData(pathToData);

            if (canChange.IsSuccess)
            {
                DirectoryPath oldPathToData = PathToData;
                PathToData = pathToData;
                AddDomainEvent(new PathToDataChangedDomainEvent(oldPathToData, pathToData));
            }

            return canChange;
        }
    }
}
