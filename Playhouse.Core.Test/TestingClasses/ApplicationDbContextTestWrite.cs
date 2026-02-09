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
                Options =
                {
                    AcceptDownloads = true,
                    Channel = BrowserChannels.ChromeBeta.ToString(),
                    ChromiumSandbox = true,
                    DownloadsPath = "D://path",
                    Headless = false,
                    SlowMo = 2
                }
            };

            await context.BrowserProfiles.AddAsync(newProfile, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);

            context.ChangeTracker.Clear();

            BrowserProfile profile = await context.BrowserProfiles.OrderBy(p => p.Id).LastAsync(CancellationToken.None);
            Assert.AreEqual("NewProfile", profile.Name);
            Assert.IsTrue(profile.Options.AcceptDownloads);
            Assert.AreEqual(BrowserChannels.ChromeBeta.ToString(), profile.Options.Channel);
            Assert.IsTrue(profile.Options.ChromiumSandbox);
            Assert.AreEqual("D://path", profile.Options.DownloadsPath);
            Assert.IsFalse(profile.Options.Headless);
            Assert.AreEqual(2, profile.Options.SlowMo);
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
