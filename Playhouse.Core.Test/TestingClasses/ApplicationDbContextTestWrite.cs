using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Test.Tools;

namespace Playhouse.Core.Test.TestingClasses
{
    [TestClass]
    public sealed class ApplicationDbContextTestWrite
    {
        [TestMethod]
        public async Task AddNewBrowserProfile()
        {
            using ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            context.Database.BeginTransaction();
            BrowserProfile newProfile = new() 
            { 
                Name = "NewProfile", 
                AcceptDownloads = true, 
                Headless = true, 
                ChromiumSandbox = null, 
                Channel = Channel.ChromeBeta.ToString(), 
                DownloadsPath = "D://path",
                SlowMo = 2
            };

            await context.BrowserProfiles.AddAsync(newProfile, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);

            context.ChangeTracker.Clear();

            BrowserProfile profile = await context.BrowserProfiles.SingleAsync(p => p.Id == 3, CancellationToken.None);
            Assert.AreEqual("NewProfile", profile.Name);
            Assert.IsTrue(profile.AcceptDownloads);
            Assert.AreEqual("D://path", profile.DownloadsPath);
            Assert.IsNull(profile.ChromiumSandbox);
            Assert.AreEqual(2, profile.SlowMo);
            Assert.IsTrue(profile.Headless);
            Assert.AreEqual(Channel.ChromeBeta.ToString(), profile.Channel);
        }

        [TestMethod]
        public async Task AddNewBotInfo()
        {
            using ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            context.Database.BeginTransaction();
            BotInfo newBot = new()
            {
                Name = "NewBot",
                Browser = BrowserType.Chromium,
            };

            await context.BotsInfo.AddAsync(newBot, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);

            context.ChangeTracker.Clear();

            BotInfo bot = await context.BotsInfo.OrderBy(b => b.Id).LastAsync(CancellationToken.None);
            Assert.AreEqual("NewBot", bot.Name);
            Assert.AreEqual(BrowserType.Chromium, bot.Browser);
            Assert.HasCount(0, bot.BrowserEvents);
        }

        [TestMethod]
        public async Task ChangePropertiesObjectFromDb()
        {
            using ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            context.Database.BeginTransaction();
            BotInfo bot = await context.BotsInfo.SingleAsync(b => b.Id == 2, CancellationToken.None);

            bot.Name = "2bot";
            await context.SaveChangesAsync(CancellationToken.None);

            context.ChangeTracker.Clear();
            bot = await context.BotsInfo.SingleAsync(b => b.Id == 2, CancellationToken.None);
            Assert.AreEqual("2bot", bot.Name);
        }
    }
}
