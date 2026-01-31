using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Test.Tools;

namespace Playhouse.Core.Test.TestingClasses
{
    [TestClass]
    public sealed class ApplicationDbContextTestRead
    {
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            DbFactory.Clear();
        }

        [TestMethod]
        public async Task ToListAsync_ReadAllTableBrowserProfile_AllObjectNotNullAndUnique()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            List<BrowserProfile> profiles = await context.BrowserProfiles.ToListAsync(CancellationToken.None);

            CollectionAssert.AllItemsAreNotNull(profiles);
            CollectionAssert.AllItemsAreUnique(profiles);
            Assert.HasCount(2, profiles);
        }

        [TestMethod]
        public async Task ToListAsync_ReadAllTableBotInfo_AllObjectNotNullAndUnique()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            List<BotInfo> bots = await context.BotsInfo.ToListAsync(CancellationToken.None);

            CollectionAssert.AllItemsAreNotNull(bots);
            CollectionAssert.AllItemsAreUnique(bots);
            Assert.HasCount(3, bots);
        }

        [TestMethod]
        public async Task SingleAsync_ReadOneBrowserProfile_PopertiesSameAsWhenWrite()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BrowserProfile profile = await context.BrowserProfiles.SingleAsync(p => p.Id == 1, CancellationToken.None);

            Assert.AreEqual("Profile1", profile.Name);
            Assert.IsNull(profile.AcceptDownloads);
            Assert.AreEqual("C://Downloads", profile.DownloadsPath);
            Assert.AreEqual(1, profile.SlowMo);
            Assert.IsFalse(profile.Headless);
        }

        [TestMethod]
        public async Task SingleAsync_ReadOneBotInfo_PopertiesSameAsWhenWrite()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BotInfo bot = await context.BotsInfo.Include(b => b.BrowserEvents).SingleAsync(b => b.Id == 2, CancellationToken.None);

            Assert.AreEqual("2", bot.Name);
            Assert.AreEqual(BrowserType.Chromium, bot.Browser);
            Assert.HasCount(2, bot.BrowserEvents);
            Assert.AreEqual(10, ((LocatorClickBrowserEvent)bot.BrowserEvents[1]).ClickOptions.Position!.X);
        }

        [TestMethod]
        public async Task SingleAsync_GetNotExistObject_Throw()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await context.BrowserProfiles.SingleAsync(p => p.Id == 3, CancellationToken.None));
        }
    }
}
