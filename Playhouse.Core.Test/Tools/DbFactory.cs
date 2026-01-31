using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.Core.Test.Tools
{
    public static class DbFactory
    {
        private static readonly List<IDisposable> _dbHandlers = [
            new SimpleAppDbHandler(),
            new TransactionAppDbHandler(),
            new RefreshingAppDbHander()
            ];

        public static ApplicationDbContext GetSimpleAppContext()
        {
            return _dbHandlers.OfType<SimpleAppDbHandler>().First().CreateDbContext();
        }

        public static ApplicationDbContext GetTransactionAppContext()
        {
            return _dbHandlers.OfType<TransactionAppDbHandler>().First().CreateDbContext();
        }

        public static ApplicationDbContext GetRefreshingAppContext()
        {
            RefreshingAppDbHander handler = _dbHandlers.OfType<RefreshingAppDbHander>().First();
            handler.Refresh();
            return handler.CreateDbContext();
        }

        public static void Clear()
        {
            _dbHandlers.ForEach(i => i.Dispose());
        }

        private abstract class DbHandlerBase<T> : IDisposable where T : DbContext
        {
            private bool _disposed;
            protected readonly string _connectionString;

            public DbHandlerBase(string connectionString)
            {
                _connectionString = connectionString;
                using T dbContext = CreateDbContext();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            public abstract T CreateDbContext();

            public abstract void SetData(DbContext context, bool _);

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed) return;

                _disposed = true;
                using T dbContext = CreateDbContext();
                dbContext.Database.EnsureDeleted();
            }

            ~DbHandlerBase()
            {
                Dispose(false);
            }
        }

        private abstract class AppDbHanderBase : DbHandlerBase<ApplicationDbContext>
        {
            public AppDbHanderBase(string connectionString) 
                : base(connectionString) 
            { }

            public override ApplicationDbContext CreateDbContext()
            {
                return new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(_connectionString)
                    .UseSeeding(SetData)
                    .LogTo(Log)
                    .EnableSensitiveDataLogging()
                    .Options);
            }

            private void Log(string message)
            {
                Debug.WriteLine(message);
            }

            public override void SetData(DbContext context, bool _)
            {
                BotInfo[] botsInfo =
                [
                    new() { Name = "test", Browser = Enums.BrowserType.WebKit },
                    new() { Name = "2", Browser = Enums.BrowserType.Chromium },
                    new() { Name = "Play", Browser = Enums.BrowserType.Firefox }
                ];
                botsInfo[0].BrowserEvents.Add(new PageCreatedBrowserEvent() { BotInfo = botsInfo[0] });
                botsInfo[1].BrowserEvents.Add(new BrowserContextClosedBrowserEvent(new BrowserContextCloseOptions() { Reason = "Причина" }) { BotInfo = botsInfo[1] });
                botsInfo[1].BrowserEvents.Add(new LocatorClickBrowserEvent(new LocatorClickOptions() { Position = new Position() { X = 10, Y = 1} }) { BotInfo = botsInfo[1] });
                context.Set<BotInfo>().AddRange(botsInfo);
                context.Set<BrowserProfile>().AddRange(
                    new BrowserProfile() { Name = "Profile1", AcceptDownloads = null, DownloadsPath = "C://Downloads", SlowMo = 1, Headless = false },
                    new BrowserProfile() { Name = "Profile1", AcceptDownloads = null, DownloadsPath = null, SlowMo = null, Headless = null });
                context.SaveChanges();
            }
        }

        private class SimpleAppDbHandler : AppDbHanderBase
        {
            private const string CONNECTIONSTRING = "Data Source=SimpleDb";
            public SimpleAppDbHandler() :
                base(CONNECTIONSTRING)
            { }

            public override ApplicationDbContext CreateDbContext()
            {
                ApplicationDbContext context = base.CreateDbContext();
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return context;
            }
        }

        private class TransactionAppDbHandler : AppDbHanderBase
        {
            private const string CONNECTIONSTRING = "Data Source=TransactionDb";

            public TransactionAppDbHandler() 
                : base(CONNECTIONSTRING)
            { }
        }

        private class RefreshingAppDbHander : AppDbHanderBase
        {
            private const string CONNECTIONSTRING = "Data Source=RefreshingDb";

            public RefreshingAppDbHander()
                : base(CONNECTIONSTRING) 
            { }

            public void Refresh()
            {
                using ApplicationDbContext context = CreateDbContext();
                context.BrowserProfiles.RemoveRange(context.BrowserProfiles.ToList());
                context.BotsInfo.RemoveRange(context.BotsInfo);
                SetData(context, false);
                context.SaveChanges();
            }
        }
    }
}
