using FluentValidation;
using Playhouse.Settings.Application.DTO;
using Playhouse.Settings.Application.Resources.Strings;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;
using Playhouse.SharedKernel.Application.Validation.FluentValidators;

namespace Playhouse.Settings.Application.Validation.Validators
{
    public sealed class ApplicationSettingsDtoValidator : AbstractValidator<ApplicationSettingsDto>
    {
        public ApplicationSettingsDtoValidator()
        {
            RuleFor(s => s.CultureName)
                .ValidValueObject(Culture.CanCreate);

            RuleFor(s => s.PathToData)
                .ValidValueObject(DirectoryPath.CanCreate);

            RuleFor(s => s.Browsers)
                .NotNull().WithMessage(ValidationMessages.ApplicationSettingsDTOValidatorBrowsersIsNull)
                .ForEach(b =>
                {
                    b.NotNull().WithMessage(ValidationMessages.ApplicationSettingsDTOValidatorBrowsersContainsNull)
                        .IsSmartEnum<IEnumerable<string>, BrowserType, int>().WithMessage(ValidationMessages.ApplicationSettingsDTOValidatorBrowserNotSupported);
                });

            RuleFor(s => s.Channels)
                .NotNull().WithMessage(ValidationMessages.ApplicationSettingsDTOValidatorChannelsIsNull)
                .ForEach(c =>
                {
                    c.NotNull().WithMessage(ValidationMessages.ApplicationSettingsDTOValidatorChannelsContainsNull)
                        .IsSmartEnum<IEnumerable<string>, BrowserChannel, int>().WithMessage(ValidationMessages.ApplicationSettingsDTOValidatorChannelNotSupported);
                });
        }
    }
}
