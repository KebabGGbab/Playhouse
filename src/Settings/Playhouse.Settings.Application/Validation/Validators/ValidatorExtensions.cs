using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Playhouse.Settings.Application.DTO;

namespace Playhouse.Settings.Application.Validation.Validators
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSingleton<IValidator<ApplicationSettingsDto>, ApplicationSettingsDtoValidator>();

            return services;
        }
    }
}
