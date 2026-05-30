using FluentValidation;
using FluentValidation.Results;
using KebabGGbab.Localization.Manager;
using Playhouse.Settings.Application.DTO;
using Playhouse.Settings.Application.Validation.Errors;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Application.Services.Settings
{
    public sealed class ApplicationSettingsService : IApplicationSettingsService
    {
        private readonly ILocalizationManager _localization;
        private readonly IApplicationSettingsRepository _repository;
        private readonly IValidator<ApplicationSettingsDto> _validator;

        private ApplicationSettings _settings;

        public string CurrentCulture => _settings.Culture.Code;

        public string PathToData => _settings.PathToData.Path;

        public IEnumerable<string> BrowserTypes => _settings.Browsers.Select(browser => browser.Name);

        public IEnumerable<string> BrowserChannels => _settings.Channels.Select(channel => channel.Name);

        public event EventHandler<IApplicationSettingsService, EventArgs>? SettingsChanged;

        public ApplicationSettingsService(IApplicationSettingsRepository repository, ILocalizationManager localization, IValidator<ApplicationSettingsDto> validator)
        {
            ArgumentNullException.ThrowIfNull(repository);
            ArgumentNullException.ThrowIfNull(localization);
            ArgumentNullException.ThrowIfNull(validator);

            _localization = localization;
            _repository = repository;
            _validator = validator;
            _settings = ApplicationSettings.Create().Value;
        }

        public async Task LoadAsync(CancellationToken token = default)
        {
            _settings = await _repository.GetAsync(token) ?? ApplicationSettings.Create().Value;
            OnSettingsChanged();
        }

        public async Task<Result> SaveAsync(ApplicationSettingsDto settings, CancellationToken token = default)
        {
            ValidationResult validationResult = _validator.Validate(settings);

            if (validationResult.IsValid == false)
            {
                return Result.Fail(validationResult.Errors.Select(e => 
                    new ValidationError(e.ErrorMessage, new Dictionary<string, object> { { nameof(e.PropertyName), e.PropertyName } })));
            }

            _settings.ChangeCulture(Culture.Create(settings.CultureName).Value);
            _settings.ChangePathToData(DirectoryPath.Create(settings.PathToData).Value);
            List<BrowserType> browsers = settings.Browsers.Select(b => BrowserType.FromName(b)).ToList();
            foreach (BrowserType browser in BrowserType.List)
            {
                bool isContainsInSettings = _settings.Browsers.Contains(browser);
                bool isContainsInChanges = browsers.Contains(browser);

                if (isContainsInSettings && !isContainsInChanges)
                {
                    _settings.RemoveBrowser(browser);
                }
                else if (!isContainsInSettings && isContainsInChanges)
                {
                    _settings.AddBrowser(browser);
                }
            }
            List<BrowserChannel> channels = settings.Channels.Select(c => BrowserChannel.FromName(c)).ToList();
            foreach (BrowserChannel channel in BrowserChannel.List)
            {
                bool isContainsInSettings = _settings.Channels.Contains(channel);
                bool isContainsInChanges = channels.Contains(channel);

                if (isContainsInSettings && !isContainsInChanges)
                {
                    _settings.RemoveChannel(channel);
                }
                else if (!isContainsInSettings && isContainsInChanges)
                {
                    _settings.AddChannel(channel);
                }
            }
            await _repository.SaveAsync(_settings, token);
            OnSettingsChanged();

            return Result.Ok();
        }

        private void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
