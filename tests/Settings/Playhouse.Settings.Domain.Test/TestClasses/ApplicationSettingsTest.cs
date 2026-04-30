using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class ApplicationSettingsTest
    {
        [TestMethod]
        public void Create_AllArgumentsAreNull_SettingsWithDefaultValues()
        {
            Result<ApplicationSettings> result = ApplicationSettings.Create();

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(Culture.Default, result.Value.Culture);
            Assert.AreEqual(DirectoryPath.Default, result.Value.PathToData);
            Assert.IsEmpty(result.Value.Browsers);
            Assert.IsEmpty(result.Value.Channels);
        }

        [TestMethod]
        public void Create_AllArgumentPassed_SettingsWithPassedArgumnents()
        {
            Culture culture = Culture.Create("en").Value;
            DirectoryPath directoryPath = DirectoryPath.Create(@"C:\Documents").Value;
            IEnumerable<BrowserType> browsers = [BrowserType.Chromium, BrowserType.WebKit];
            IEnumerable<BrowserChannel> channels = [BrowserChannel.Chromium, BrowserChannel.Chrome, BrowserChannel.Msedge];

            Result<ApplicationSettings> result = ApplicationSettings.Create(culture, directoryPath, browsers, channels);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(culture, result.Value.Culture);
            Assert.AreEqual(directoryPath, result.Value.PathToData);
            Assert.HasCount(2, result.Value.Browsers);
            Assert.Contains(BrowserType.Chromium, result.Value.Browsers);
            Assert.Contains(BrowserType.WebKit, result.Value.Browsers);
            Assert.HasCount(3, result.Value.Channels);
            Assert.Contains(BrowserChannel.Chromium, result.Value.Channels);
            Assert.Contains(BrowserChannel.Chrome, result.Value.Channels);
            Assert.Contains(BrowserChannel.Msedge, result.Value.Channels);
        }

        [TestMethod]
        public void CanAddBrowser_Valid_Ok()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanAddBrowser(BrowserType.Firefox);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(settings.Browsers);
        }

        [TestMethod]
        public void CanAddBrowser_BrowserIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanAddBrowser(null!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Browsers);
        }

        [TestMethod]
        public void CanAddBrowser_BrowserAlreadyContained_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create(browsers: [BrowserType.WebKit]).Value;

            Result result = settings.CanAddBrowser(BrowserType.WebKit);

            Assert.IsTrue(result.IsFailure);
            Assert.ContainsSingle(settings.Browsers);
            Assert.Contains(BrowserType.WebKit, settings.Browsers);
        }

        [TestMethod]
        public void AddBrowser_CollectionIsEmpty_BrowserAdded()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.AddBrowser(BrowserType.Chromium);

            Assert.IsTrue(result.IsSuccess);
            Assert.ContainsSingle(settings.Browsers);
            Assert.Contains(BrowserType.Chromium, settings.Browsers);
        }

        [TestMethod]
        public void AddBrowser_CollectionIsNotEmpty_BrowserAdded()
        {
            ApplicationSettings settings = ApplicationSettings.Create(browsers: [BrowserType.Firefox]).Value;

            Result result = settings.AddBrowser(BrowserType.Chromium);

            Assert.IsTrue(result.IsSuccess);
            Assert.HasCount(2, settings.Browsers);
            Assert.Contains(BrowserType.Firefox, settings.Browsers);
            Assert.Contains(BrowserType.Chromium, settings.Browsers);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Firefox")]
        public void AddBrowser_NotValid_Fail(string? browserName)
        {
            BrowserType.TryFromName(browserName, out BrowserType? browser);
            ApplicationSettings settings = ApplicationSettings.Create(browsers: [BrowserType.Firefox]).Value;

            Result result = settings.AddBrowser(browser!);

            Assert.IsTrue(result.IsFailure);
            Assert.ContainsSingle(settings.Browsers);
            Assert.Contains(BrowserType.Firefox, settings.Browsers);
        }

        [TestMethod]
        public void CanRemoveBrowser_Valid_Ok()
        {
            ApplicationSettings settings = ApplicationSettings.Create(browsers: [BrowserType.Firefox]).Value;

            Result result = settings.CanRemoveBrowser(BrowserType.Firefox);

            Assert.IsTrue(result.IsSuccess);
            Assert.ContainsSingle(settings.Browsers);
            Assert.Contains(BrowserType.Firefox, settings.Browsers);
        }

        [TestMethod]
        public void CanRemoveBrowser_BrowserIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanRemoveBrowser(null!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Browsers);
        }

        [TestMethod]
        public void CanRemoveBrowser_BrowserIsMissing_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanRemoveBrowser(BrowserType.Chromium);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Browsers);
        }

        [TestMethod]
        public void RemoveBrowser_AfterRemovedColectionIsEmpty_BrowserRemoved()
        {
            ApplicationSettings settings = ApplicationSettings.Create(browsers: [BrowserType.WebKit]).Value;

            Result result = settings.RemoveBrowser(BrowserType.WebKit);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(settings.Browsers);
        }

        [TestMethod]
        public void RemoveBrowser_AfterRemovedColectionIsNotEmpty_BrowserRemoved()
        {
            ApplicationSettings settings = ApplicationSettings.Create(browsers: [BrowserType.WebKit, BrowserType.Firefox]).Value;

            Result result = settings.RemoveBrowser(BrowserType.WebKit);

            Assert.IsTrue(result.IsSuccess);
            Assert.ContainsSingle(settings.Browsers);
            Assert.Contains(BrowserType.Firefox, settings.Browsers);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Firefox")]
        public void RemoveBrowser_NotValid_Fail(string? browserName)
        {
            BrowserType.TryFromName(browserName, out BrowserType? browser);
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.RemoveBrowser(browser!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Browsers);
        }

        [TestMethod]
        public void CanAddChannel_Valid_Ok()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanAddChannel(BrowserChannel.Chrome);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(settings.Channels);
        }

        [TestMethod]
        public void CanAddChannel_ChannelIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanAddChannel(null!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Channels);
        }

        [TestMethod]
        public void CanAddChannel_ChannelAlreadyContained_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create(channels: [BrowserChannel.Msedge]).Value;

            Result result = settings.CanAddChannel(BrowserChannel.Msedge);

            Assert.IsTrue(result.IsFailure);
            Assert.ContainsSingle(settings.Channels);
            Assert.Contains(BrowserChannel.Msedge, settings.Channels);
        }

        [TestMethod]
        public void AddChannel_CollectionIsEmpty_ChannelAdded()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.AddChannel(BrowserChannel.ChromeBeta);

            Assert.IsTrue(result.IsSuccess);
            Assert.ContainsSingle(settings.Channels);
            Assert.Contains(BrowserChannel.ChromeBeta, settings.Channels);
        }

        [TestMethod]
        public void AddChannel_CollectionIsNotEmpty_ChannelAdded()
        {
            ApplicationSettings settings = ApplicationSettings.Create(channels: [BrowserChannel.ChromeCanary]).Value;

            Result result = settings.AddChannel(BrowserChannel.MsedgeBeta);

            Assert.IsTrue(result.IsSuccess);
            Assert.HasCount(2, settings.Channels);
            Assert.Contains(BrowserChannel.ChromeCanary, settings.Channels);
            Assert.Contains(BrowserChannel.MsedgeBeta, settings.Channels);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("chrome")]
        public void AddChannel_NotValid_Fail(string? channelName)
        {
            BrowserChannel.TryFromName(channelName, out BrowserChannel? channel);
            ApplicationSettings settings = ApplicationSettings.Create(channels: [BrowserChannel.Chrome]).Value;

            Result result = settings.AddChannel(channel!);

            Assert.IsTrue(result.IsFailure);
            Assert.ContainsSingle(settings.Channels);
            Assert.Contains(BrowserChannel.Chrome, settings.Channels);
        }

        [TestMethod]
        public void CanRemoveChannel_Valid_Ok()
        {
            ApplicationSettings settings = ApplicationSettings.Create(channels: [BrowserChannel.Msedge]).Value;

            Result result = settings.CanRemoveChannel(BrowserChannel.Msedge);

            Assert.IsTrue(result.IsSuccess);
            Assert.ContainsSingle(settings.Channels);
            Assert.Contains(BrowserChannel.Msedge, settings.Channels);
        }

        [TestMethod]
        public void CanRemoveChannel_ChannelIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanRemoveChannel(null!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Channels); 
        }

        [TestMethod]
        public void CanRemoveChannel_ChannelIsMissing_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanRemoveChannel(BrowserChannel.Msedge);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Channels);
        }

        [TestMethod]
        public void RemoveChannel_AfterRemovedColectionIsEmpty_ChannelRemoved()
        {
            ApplicationSettings settings = ApplicationSettings.Create(channels: [BrowserChannel.Msedge]).Value;

            Result result = settings.RemoveChannel(BrowserChannel.Msedge);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(settings.Channels);
        }

        [TestMethod]
        public void RemoveChannel_AfterRemovedColectionIsNotEmpty_ChannelRemoved()
        {
            ApplicationSettings settings = ApplicationSettings.Create(channels: [BrowserChannel.Chromium, BrowserChannel.MsedgeDev]).Value;

            Result result = settings.RemoveChannel(BrowserChannel.Chromium);

            Assert.IsTrue(result.IsSuccess);
            Assert.ContainsSingle(settings.Channels);
            Assert.Contains(BrowserChannel.MsedgeDev, settings.Channels);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("chrome")]
        public void RemoveChannel_NotValid_Fail(string? channelName)
        {
            BrowserChannel.TryFromName(channelName, out BrowserChannel? channel);
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.RemoveChannel(channel!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsEmpty(settings.Channels);
        }

        [TestMethod]
        public void CanChangeCulture_Valid_Ok()
        {
            ApplicationSettings settings = ApplicationSettings.Create(culture: Culture.Create("ru").Value).Value;
            Culture newCulture = Culture.Create("en").Value;

            Result result = settings.CanCultureChange(newCulture);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreNotEqual(newCulture, settings.Culture);
        }

        [TestMethod]
        public void CanChangeCulture_NewCultureIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;
            Culture? newCulture = null;

            Result result = settings.CanCultureChange(newCulture!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(settings.Culture);
        }

        [TestMethod]
        public void CanChangeCulture_CultureAlreadySet_Fail()
        {
            Culture newCulture = Culture.Create("ru").Value;
            ApplicationSettings settings = ApplicationSettings.Create(culture: newCulture).Value;

            Result result = settings.CanCultureChange(newCulture);

            Assert.IsTrue(result.IsFailure);
            Assert.AreSame(newCulture, settings.Culture);
        }

        [TestMethod]
        public void ChangeCulture_Valid_CultureChanged()
        {
            ApplicationSettings settings = ApplicationSettings.Create(culture: Culture.Create("ru").Value).Value;
            Culture newCulture = Culture.Create("en").Value;

            Result result = settings.ChangeCulture(newCulture);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newCulture, settings.Culture);
        }

        [TestMethod]
        public void ChangeCulture_NewCultureIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.ChangeCulture(null!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(settings.Culture);
        }

        [TestMethod]
        public void ChangeCulture_CultureAlreadySet_ResultFail()
        {
            Culture newCulture = Culture.Create("ru").Value;
            ApplicationSettings settings = ApplicationSettings.Create(culture: newCulture).Value;

            Result result = settings.ChangeCulture(newCulture);

            Assert.IsTrue(result.IsFailure);
            Assert.AreSame(newCulture, settings.Culture);
        }

        [TestMethod]
        public void CanChangePathToData_Valid_PathToDataChanged()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;
            DirectoryPath newPath = DirectoryPath.Create(@"C:\users\myuser").Value;

            Result result = settings.CanChangePathToData(newPath);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreNotEqual(newPath, settings.PathToData);
        }

        [TestMethod]
        public void CanChangePathToData_NewPathIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.CanChangePathToData(null!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(settings.PathToData);
        }

        [TestMethod]
        public void CanChangePathToData_PathToDataAlreadySet_Fail()
        {
            DirectoryPath newPath = DirectoryPath.Create(@"C:\users\myuser").Value;
            ApplicationSettings settings = ApplicationSettings.Create(pathToData: newPath).Value;

            Result result = settings.ChangePathToData(newPath);

            Assert.IsTrue(result.IsFailure);
            Assert.AreSame(newPath, settings.PathToData);
        }

        [TestMethod]
        public void ChangePathToData_Valid_PathToDataChanged()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;
            DirectoryPath newPath = DirectoryPath.Create(@"C:\users\myuser").Value;

            Result result = settings.ChangePathToData(newPath);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreSame(newPath, settings.PathToData);
        }

        [TestMethod]
        public void ChangePathToData_NewPathIsNull_Fail()
        {
            ApplicationSettings settings = ApplicationSettings.Create().Value;

            Result result = settings.ChangePathToData(null!);

            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(settings.PathToData);
        }

        [TestMethod]
        public void ChangePathToData_PathToDataAlreadySet_ResultFail()
        {
            DirectoryPath newPath = DirectoryPath.Create(@"C:\users\myuser").Value;
            ApplicationSettings settings = ApplicationSettings.Create(pathToData: newPath).Value;

            Result result = settings.ChangePathToData(newPath);

            Assert.IsTrue(result.IsFailure);
            Assert.AreSame(newPath, settings.PathToData);
        }
    }
}