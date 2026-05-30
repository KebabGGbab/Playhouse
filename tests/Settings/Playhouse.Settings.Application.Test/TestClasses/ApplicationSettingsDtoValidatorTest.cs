using FluentValidation.TestHelper;
using Playhouse.Settings.Application.DTO;
using Playhouse.Settings.Application.Validation.Validators;

namespace Playhouse.Settings.Application.Test.TestClasses
{
    [TestClass]
    public class ApplicationSettingsDtoValidatorTest
    {
        private readonly ApplicationSettingsDtoValidator _validator;

        public ApplicationSettingsDtoValidatorTest()
        {
            _validator = new ApplicationSettingsDtoValidator();
        }

        [TestMethod]
        public void Validate_WithoutError_IsValid()
        {
            ApplicationSettingsDto settings = new("en", @"C:\", ["Chromium"], ["msedge"]);

            TestValidationResult<ApplicationSettingsDto> result = _validator.TestValidate(settings);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        [DataRow(null, "Язык пользовательского интерфейса не указан.")]
        [DataRow("ja", "Язык 'ja' не поддерживается приложением.")]
        public void Validate_CultureWithError_IsNotValid(string culture, string error)
        {
            ApplicationSettingsDto settings = new(culture, @"C:\", ["Chromium"], ["msedge"]);

            TestValidationResult<ApplicationSettingsDto> result = _validator.TestValidate(settings);

            result.ShouldHaveValidationErrorFor(s => s.CultureName)
                .WithErrorMessage(error)
                .Only();
        }

        [TestMethod]
        [DataRow(null, "Путь к каталогу не указан.")]
        [DataRow(@"\mydirectory", "Путь к каталогу должен быть абсолютным.")]
        public void Validate_PathToDataWithError_IsNotValid(string path, string error)
        {
            ApplicationSettingsDto settings = new("ru", path, ["Chromium"], ["msedge"]);

            TestValidationResult<ApplicationSettingsDto> result = _validator.TestValidate(settings);

            result.ShouldHaveValidationErrorFor(s => s.PathToData)
                .WithErrorMessage(error)
                .Only();
        }

        [TestMethod]
        public void Validate_BrowsersIsNull_Throw()
        {
            void action() => new ApplicationSettingsDto("ru", @"C:\", null!, ["msedge"]);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Validate_BrowsersElementIsNull_IsNotValid()
        {
            ApplicationSettingsDto settings = new("ru", @"C:\", [null!], ["msedge"]);

            TestValidationResult<ApplicationSettingsDto> result = _validator.TestValidate(settings);

            result.ShouldHaveValidationErrorFor("Browsers[0]")
                .WithErrorMessage("Коллекция браузера не может содержать значение null.")
                .Only();
        }

        [TestMethod]
        public void Validate_BrowsersElementIsNotSupported_IsNotValid()
        {
            ApplicationSettingsDto settings = new("ru", @"C:\", ["MyBrowser"], ["msedge"]);

            TestValidationResult<ApplicationSettingsDto> result = _validator.TestValidate(settings);

            result.ShouldHaveValidationErrorFor("Browsers[0]")
                .WithErrorMessage("Веб-браузер 'MyBrowser' не поддерживается приложением.")
                .Only();
        }

        [TestMethod]
        public void Validate_ChannelsIsNull_Throw()
        {
            void action() => new ApplicationSettingsDto("ru", @"C:\", ["Chromium"], null!);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Validate_ChannelsElementIsNull_IsNotValid()
        {
            ApplicationSettingsDto settings = new("ru", @"C:\", ["Chromium"], [null!]);

            TestValidationResult<ApplicationSettingsDto> result = _validator.TestValidate(settings);

            result.ShouldHaveValidationErrorFor("Channels[0]")
                .WithErrorMessage("Коллекция каналов браузера не может содержать значение null.")
                .Only();
        }

        [TestMethod]
        public void Validate_BrowserElementIsNotSupported_IsNotValid()
        {
            ApplicationSettingsDto settings = new("ru", @"C:\", ["Chromium"], ["my-channel"]);

            TestValidationResult<ApplicationSettingsDto> result = _validator.TestValidate(settings);

            result.ShouldHaveValidationErrorFor("Channels[0]")
                .WithErrorMessage("Канал веб-браузер 'my-channel' не поддерживается приложением.")
                .Only();
        }
    }
}
