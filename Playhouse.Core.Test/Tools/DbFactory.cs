using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;

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
                    .Options);
            }

            public override void SetData(DbContext context, bool _)
            {
                context.Set<BotInfo>().AddRange(
                    new() { Name = "test", Browser = BrowserType.WebKit },
                    new() { Name = "2", Browser = BrowserType.Chromium },
                    new() { Name = "Play", Browser = BrowserType.Firefox });
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
