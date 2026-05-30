using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Playhouse.Settings.Application.DTO;
using Playhouse.Settings.Application.Validation.Validators;

namespace Playhouse.Settings.Application.Test.TestClasses
{
    [TestClass]
    public sealed class ValidatorExtensionsTest
    {
        [TestMethod]
        public void AddValidators_Simple_ValidationsAdded()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddValidators();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IValidator<ApplicationSettingsDto>? validator = serviceProvider.GetService<IValidator<ApplicationSettingsDto>>();
            Assert.IsNotNull(validator);
            Assert.IsInstanceOfType<ApplicationSettingsDtoValidator>(validator);
        }

        [TestMethod]
        public void AddValidators_ServiceCollectionIsNull_Throw()
        {
            IServiceCollection services = null!;

            Assert.ThrowsExactly<ArgumentNullException>(() => services.AddValidators());
        }

        [TestMethod]
        public void AddValidators_Simple_ReturnSameServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            IServiceCollection returnedServices = services.AddValidators();

            Assert.AreEqual(services, returnedServices);
        }
    }
}
