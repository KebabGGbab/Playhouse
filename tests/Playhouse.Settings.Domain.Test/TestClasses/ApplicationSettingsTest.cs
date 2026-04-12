using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class ApplicationSettingsTest
    {
        [TestMethod]
        public void Create_AllArgumentsAreNull_SettingsWithDefaultValues()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create();

            Assert.IsNotNull(applicationSettings);
            Assert.AreEqual(Culture.Default, applicationSettings.Culture);
            Assert.AreEqual(DirectoryPath.Default, applicationSettings.PathToData);
            Assert.HasCount(0, applicationSettings.Browsers);
            Assert.HasCount(0, applicationSettings.Channels);
        }

        [TestMethod]
        public void Create_AllArgumentPassed_SettingsWithPassedArgumnents()
        {
            Culture culture = Culture.Create("en").Value!;
            DirectoryPath directoryPath = DirectoryPath.Create(@"C:\Documents").Value!;
            IEnumerable<BrowserType> browsers = [BrowserType.Chromium, BrowserType.WebKit];
            IEnumerable<BrowserChannel> channels = [BrowserChannel.Chromium, BrowserChannel.Chrome, BrowserChannel.Msedge];

            ApplicationSettings applicationSettings = ApplicationSettings.Create(culture, directoryPath, browsers, channels);

            Assert.AreEqual(culture, applicationSettings.Culture);
            Assert.AreEqual(directoryPath, applicationSettings.PathToData);
            Assert.HasCount(2, applicationSettings.Browsers);
            Assert.Contains(BrowserType.Chromium, applicationSettings.Browsers);
            Assert.Contains(BrowserType.WebKit, applicationSettings.Browsers);
            Assert.HasCount(3, applicationSettings.Channels);
            Assert.Contains(BrowserChannel.Chromium, applicationSettings.Channels);
            Assert.Contains(BrowserChannel.Chrome, applicationSettings.Channels);
            Assert.Contains(BrowserChannel.Msedge, applicationSettings.Channels);
        }

        [TestMethod]
        public void AddBrowser_Simple_BrowserAdded()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(browsers: [BrowserType.Firefox]);

            Result<ApplicationSettings> result = applicationSettings.AddBrowser(BrowserType.Chromium);

            Assert.HasCount(2, applicationSettings.Browsers);
            Assert.Contains(BrowserType.Chromium, applicationSettings.Browsers);
            Assert.HasCount(0, result.Errors);
        }

        [TestMethod]
        public void AddBrowser_BrowserIsNull_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(browsers: [BrowserType.Firefox]);

            Result<ApplicationSettings> result = applicationSettings.AddBrowser(null!);

            Assert.HasCount(1, applicationSettings.Browsers);
            Assert.Contains("Браузер не указан.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void AddBrowser_BrowserAlreadyContainedInCollection_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(browsers: [BrowserType.WebKit, BrowserType.Chromium]);

            Result<ApplicationSettings> result = applicationSettings.AddBrowser(BrowserType.WebKit);

            Assert.HasCount(2, applicationSettings.Browsers);
            Assert.Contains("Браузер уже добавлен.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void RemoveBrowser_Simple_BrowserRemoved()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(browsers: [BrowserType.WebKit, BrowserType.Firefox]);

            Result<ApplicationSettings> result = applicationSettings.RemoveBrowser(BrowserType.WebKit);

            Assert.HasCount(1, applicationSettings.Browsers);
            Assert.DoesNotContain(BrowserType.WebKit, applicationSettings.Browsers);
            Assert.HasCount(0, result.Errors);
        }

        [TestMethod]
        public void RemoveBrowser_BrowserIsNull_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(browsers: [BrowserType.Firefox]);

            Result<ApplicationSettings> result = applicationSettings.RemoveBrowser(null!);

            Assert.HasCount(1, applicationSettings.Browsers);
            Assert.Contains("Браузер не указан.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void RemoveBrowser_BrowserIsNotContainedInCollection_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(browsers: [BrowserType.Firefox]);

            Result<ApplicationSettings> result = applicationSettings.RemoveBrowser(BrowserType.Chromium);

            Assert.HasCount(1, applicationSettings.Browsers);
            Assert.Contains("Браузер отсутствует.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void AddChannel_Simple_ChannelAdded()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(channels: [BrowserChannel.Chrome, BrowserChannel.ChromeDev, BrowserChannel.MsedgeCanary]);

            Result<ApplicationSettings> result = applicationSettings.AddChannel(BrowserChannel.ChromeBeta);

            Assert.HasCount(4, applicationSettings.Channels);
            Assert.Contains(BrowserChannel.ChromeBeta, applicationSettings.Channels);
            Assert.HasCount(0, result.Errors);
        }

        [TestMethod]
        public void AddChannel_ChannelIsNull_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(channels: [BrowserChannel.Chrome, BrowserChannel.ChromeDev, BrowserChannel.MsedgeCanary]);

            Result<ApplicationSettings> result = applicationSettings.AddChannel(null!);

            Assert.HasCount(3, applicationSettings.Channels);
            Assert.Contains("Канал не указан.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void AddChannel_ChannelAlreadyContainedInCollection_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(channels: [BrowserChannel.Chrome, BrowserChannel.ChromeDev, BrowserChannel.MsedgeCanary]);

            Result<ApplicationSettings> result = applicationSettings.AddChannel(BrowserChannel.Chrome);

            Assert.HasCount(3, applicationSettings.Channels);
            Assert.Contains("Канал уже добавлен.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void RemoveChannel_Simple_BrowserRemoved()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(channels: [BrowserChannel.Chrome, BrowserChannel.ChromeDev, BrowserChannel.MsedgeCanary]);

            Result<ApplicationSettings> result = applicationSettings.RemoveChannel(BrowserChannel.MsedgeCanary);

            Assert.HasCount(2, applicationSettings.Channels);
            Assert.DoesNotContain(BrowserChannel.MsedgeCanary, applicationSettings.Channels);
            Assert.HasCount(0, result.Errors);
        }

        [TestMethod]
        public void RemoveChannel_ChannelIsNull_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(channels: [BrowserChannel.Chrome, BrowserChannel.ChromeDev, BrowserChannel.MsedgeCanary]);

            Result<ApplicationSettings> result = applicationSettings.RemoveChannel(null!);

            Assert.HasCount(3, applicationSettings.Channels);
            Assert.Contains("Канал не указан.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void RemoveChannel_ChannelIsNotContainedInCollection_ResultFail()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.Create(channels: [BrowserChannel.Chrome, BrowserChannel.ChromeDev, BrowserChannel.MsedgeCanary]);

            Result<ApplicationSettings> result = applicationSettings.RemoveChannel(BrowserChannel.Chromium);

            Assert.HasCount(3, applicationSettings.Channels);
            Assert.Contains("Канал отсутствует.", result.Errors!);
            Assert.HasCount(1, result.Errors);
        }

        [TestMethod]
        public void ChangeCulture_Simple_CultureChanged()
        {
            ApplicationSettings settings = ApplicationSettings.Create();
            Culture culture = Culture.Create("ru").Value!;

            Result<ApplicationSettings> result = settings.ChangeCulture(culture);

            Assert.HasCount(0, result.Errors);
            Assert.AreEqual(culture, settings.Culture);
        }

        [TestMethod]
        public void ChangeCulture_NewCultureIsNull_ResultFail()
        {
            ApplicationSettings settings = ApplicationSettings.Create();

            Result<ApplicationSettings> result = settings.ChangeCulture(null!);

            Assert.HasCount(1, result.Errors);
            Assert.Contains("Культура не указана.", result.Errors);
        }

        [TestMethod]
        public void ChangeCulture_NewAndOldValuesIdentical_ResultFail()
        {
            ApplicationSettings settings = ApplicationSettings.Create();

            Result<ApplicationSettings> result = settings.ChangeCulture(Culture.Default);

            Assert.HasCount(1, result.Errors);
            Assert.Contains("Новое значение культуры совпадает с текущим.", result.Errors);
        }

        [TestMethod]
        public void ChangePathToData_Simple_PathToDataChanged()
        {
            ApplicationSettings settings = ApplicationSettings.Create();
            DirectoryPath path = DirectoryPath.Create(@"C:\users\myuser").Value!;

            Result<ApplicationSettings> result = settings.ChangePathToData(path);

            Assert.HasCount(0, result.Errors);
            Assert.AreEqual(path, settings.PathToData);
        }

        [TestMethod]
        public void ChangePathToData_NewPathIsNull_ResultFail()
        {
            ApplicationSettings settings = ApplicationSettings.Create();

            Result<ApplicationSettings> result = settings.ChangePathToData(null!);

            Assert.HasCount(1, result.Errors);
            Assert.Contains("Путь к каталогу с данными не указан.", result.Errors);
        }

        [TestMethod]
        public void ChangePathToData_NewAndOldValuesIdentical_ResultFail()
        {
            ApplicationSettings settings = ApplicationSettings.Create();

            Result<ApplicationSettings> result = settings.ChangePathToData(DirectoryPath.Default);

            Assert.HasCount(1, result.Errors);
            Assert.Contains("Новое значение пути к данным совпадает с текущим.", result.Errors);
        }
    }
}
