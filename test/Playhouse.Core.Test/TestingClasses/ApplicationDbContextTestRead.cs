using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using Playhouse.Core.Data;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions;
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
        public async Task GetNotExistObject_Throw()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await context.Profiles.SingleAsync(p => p.Id == 4, CancellationToken.None));
        }

        [TestMethod]
        public async Task ReadAllTableProfile_AllObjectNotNullAndUnique()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            List<BrowserConfiguration> profiles = await context.Profiles.ToListAsync(CancellationToken.None);

            CollectionAssert.AllItemsAreNotNull(profiles);
            CollectionAssert.AllItemsAreUnique(profiles);
            Assert.HasCount(3, profiles);
        }

        [TestMethod]
        public async Task ReadOneProfile_PopertiesSameAsWhenWrite()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BrowserConfiguration profile = await context.Profiles.SingleAsync(p => p.Id == 1, CancellationToken.None);

            Assert.AreEqual("test", profile.Name);
            Assert.IsFalse(profile.Options.AcceptDownloads);
            Assert.AreEqual(BrowserChannels.ChromeBeta.ToString(), profile.Options.Channel);
            Assert.IsFalse(profile.Options.ChromiumSandbox);
            Assert.IsNull(profile.Options.DownloadsPath);
            Assert.IsTrue(profile.Options.Headless);
            Assert.AreEqual(1, profile.Options.SlowMo);
        }

        [TestMethod]
        public async Task BrowserTypeLaunchPersistentContextOptionsStrictDecorator_DefaultValueAllProperties()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BrowserConfiguration profile = await context.Profiles.SingleAsync(p => p.Id == 2, CancellationToken.None);

            Assert.IsTrue(profile.Options.AcceptDownloads);
            Assert.IsNull(profile.Options.Channel);
            Assert.IsFalse(profile.Options.ChromiumSandbox);
            Assert.IsNull(profile.Options.DownloadsPath);
            Assert.IsTrue(profile.Options.Headless);
            Assert.IsNull(profile.Options.SlowMo);
        }

        [TestMethod]
        public async Task BrowserTypeLaunchPersistentContextOptionsStrictDecorator_NotDefaultValueAllProperties()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BrowserConfiguration profile = await context.Profiles.SingleAsync(p => p.Id == 3, CancellationToken.None);

            Assert.IsFalse(profile.Options.AcceptDownloads);
            Assert.AreEqual(BrowserChannels.ChromeBeta.ToString(), profile.Options.Channel);
            Assert.IsTrue(profile.Options.ChromiumSandbox);
            Assert.AreEqual("C://Downloads", profile.Options.DownloadsPath);
            Assert.IsFalse(profile.Options.Headless);
            Assert.AreEqual(1, profile.Options.SlowMo);
        }

        [TestMethod]
        public async Task ReadAllTableBot_AllObjectNotNullAndUnique()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            List<BotConfiguration> bots = await context.Bots.ToListAsync(CancellationToken.None);

            CollectionAssert.AllItemsAreNotNull(bots);
            CollectionAssert.AllItemsAreUnique(bots);
            Assert.HasCount(6, bots);
        }

        [TestMethod]
        public async Task ReadOneBot_PopertiesSameAsWhenWrite()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BotConfiguration bot = await context.Bots.Include(b => b.Actions).SingleAsync(b => b.Id == 1, CancellationToken.None);

            Assert.AreEqual("test", bot.Name);
            Assert.AreEqual(Enums.BrowserType.WebKit, bot.Browser);
            Assert.HasCount(1, bot.Actions);
        }

        [TestMethod]
        public async Task ReadNestedOwnType_LocatorClick_Options_Position()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BotConfiguration bot = await context.Bots.Include(b => b.Actions).SingleAsync(b => b.Id == 2, CancellationToken.None);

            LocatorClickBotAction @event = (LocatorClickBotAction)bot.Actions[0];
            Assert.AreEqual(10, @event.Options.Position?.X);
            Assert.AreEqual(3, @event.Options.Position?.Y);
        }

        [TestMethod]
        public async Task PageClosedBotAction_And_PageCloseOptionsStrictDecorator()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BotConfiguration bot = await context.Bots.Include(b => b.Actions).SingleAsync(b => b.Id == 3, CancellationToken.None);

            List<PageClosedBotAction> events = bot.Actions
                .Select(e => (PageClosedBotAction)e)
                .OrderBy(e => e.Number)
                .ToList();
            Assert.IsPositive(events[0].Id);
            Assert.IsPositive(events[0].Number);
            Assert.IsNotNull(events[0].Bot);
            Assert.IsTrue(events[0].Options.RunBeforeUnload);
            Assert.AreEqual("Так надо", events[0].Options.Reason);
            Assert.IsFalse(events[1].Options.RunBeforeUnload);
            Assert.IsNull(events[1].Options.Reason);
            Assert.IsFalse(events[2].Options.RunBeforeUnload);
        }

        [TestMethod]
        public async Task ApplicationDbContext_And_BrowserContextCloseOptionsStrictDecorator()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BotConfiguration bot = await context.Bots.Include(b => b.Actions).SingleAsync(b => b.Id == 4, CancellationToken.None);

            List<BrowserContextClosedBotAction> events = bot.Actions
                .Select(e => (BrowserContextClosedBotAction)e)
                .OrderBy(e => e.Number)
                .ToList();
            Assert.IsPositive(events[0].Id);
            Assert.IsPositive(events[0].Number);
            Assert.IsNotNull(events[0].Bot);
            Assert.IsNull(events[0].Options.Reason);
            Assert.AreEqual("Причина", events[1].Options.Reason);
        }

        [TestMethod]
        public async Task PageGoToBotAction_And_PageGoToOptionsStrictDecorator()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BotConfiguration bot = await context.Bots.Include(b => b.Actions).SingleAsync(b => b.Id == 5, CancellationToken.None);

            List<PageGoToBotAction> events = bot.Actions
                .Select(e => (PageGoToBotAction)e)
                .OrderBy(e => e.Number)
                .ToList();

            Assert.IsPositive(events[0].Id);
            Assert.IsPositive(events[0].Number);
            Assert.IsNotNull(events[0].Bot);
            Assert.AreEqual("https://playhoouse.ru/", events[0].Url);
            Assert.IsNull(events[0].Options.Referer);
            Assert.AreEqual(30000, events[0].Options.Timeout);
            Assert.AreEqual(WaitUntilState.Load, events[0].Options.WaitUntil);
            Assert.AreEqual("Строка", events[1].Options.Referer);
            Assert.AreEqual(10000, events[1].Options.Timeout);
            Assert.AreEqual(WaitUntilState.DOMContentLoaded, events[1].Options.WaitUntil);
        }

        [TestMethod]
        public async Task LocatorClickBotAction_And_LocatorClickOptionsStrictDecorator()
        {
            using ApplicationDbContext context = DbFactory.GetSimpleAppContext();

            BotConfiguration bot = await context.Bots.Include(b => b.Actions).SingleAsync(b => b.Id == 6, CancellationToken.None);

            List<LocatorClickBotAction> events = bot.Actions
                .Select(e => (LocatorClickBotAction)e)
                .OrderBy(e => e.Number)
                .ToList();

            Assert.IsPositive(events[0].Id);
            Assert.IsPositive(events[0].Number);
            Assert.IsNotNull(events[0].Bot);
            Assert.AreEqual(MouseButton.Middle, events[0].Options.Button);
            Assert.AreEqual(3, events[0].Options.ClickCount);
            Assert.AreEqual(530, events[0].Options.Delay);
            Assert.IsTrue(events[0].Options.Force);
            Assert.AreEqual(7, events[0].Options.Steps);
            Assert.AreEqual(15000, events[0].Options.Timeout);
            Assert.IsTrue(events[0].Options.Trial);
            Assert.AreEqual(MouseButton.Left, events[1].Options.Button);
            Assert.AreEqual(1, events[1].Options.ClickCount);
            Assert.AreEqual(0, events[1].Options.Delay);
            Assert.IsFalse(events[1].Options.Force);
            Assert.AreEqual(1, events[1].Options.Steps);
            Assert.AreEqual(30000, events[1].Options.Timeout);
            Assert.IsFalse(events[1].Options.Trial);
        }
    }
}
