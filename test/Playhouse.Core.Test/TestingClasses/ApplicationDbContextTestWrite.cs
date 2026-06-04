using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.Core.Test.Tools;

namespace Playhouse.Core.Test.TestingClasses
{
    [TestClass]
    public sealed class ApplicationDbContextTestWrite
    {
        [TestMethod]
        public async Task AddNewProfile()
        {
            using ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            context.Database.BeginTransaction();
            BrowserConfiguration newProfile = new() 
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

            await context.Profiles.AddAsync(newProfile, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);

            context.ChangeTracker.Clear();

            BrowserConfiguration profile = await context.Profiles.OrderBy(p => p.Id).LastAsync(CancellationToken.None);
            Assert.AreEqual("NewProfile", profile.Name);
            Assert.IsTrue(profile.Options.AcceptDownloads);
            Assert.AreEqual(BrowserChannels.ChromeBeta.ToString(), profile.Options.Channel);
            Assert.IsTrue(profile.Options.ChromiumSandbox);
            Assert.AreEqual("D://path", profile.Options.DownloadsPath);
            Assert.IsFalse(profile.Options.Headless);
            Assert.AreEqual(2, profile.Options.SlowMo);
        }

        [TestMethod]
        public async Task AddNewBot()
        {
            using ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            context.Database.BeginTransaction();
            BotConfiguration newBot = new(BrowserTypes.Chromium)
            {
                Name = "NewBot",
            };

            await context.Bots.AddAsync(newBot, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);

            context.ChangeTracker.Clear();

            BotConfiguration bot = await context.Bots.OrderBy(b => b.Id).LastAsync(CancellationToken.None);
            Assert.AreEqual("NewBot", bot.Name);
            Assert.AreEqual(BrowserTypes.Chromium, bot.Browser);
            Assert.HasCount(0, bot.Actions);
        }

        [TestMethod]
        public async Task ChangePropertiesObjectFromDb()
        {
            using ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            context.Database.BeginTransaction();
            BotConfiguration bot = await context.Bots.SingleAsync(b => b.Id == 2, CancellationToken.None);

            bot.Name = "2bot";
            await context.SaveChangesAsync(CancellationToken.None);

            context.ChangeTracker.Clear();
            bot = await context.Bots.SingleAsync(b => b.Id == 2, CancellationToken.None);
            Assert.AreEqual("2bot", bot.Name);
        }
    }
}
