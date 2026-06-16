using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playhouse.Infrastructure.Repository;

namespace Playhouse.Infrastructure
{
    public static class DataExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, string connectionStringKey )
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddDbContextFactory<ApplicationDbContext>(
                (services, options) => options.UseSqlite(
                    services.GetRequiredService<IConfiguration>().GetConnectionString(connectionStringKey)))
                .AddSingleton<ISettingsRepository, ApplicationRepository>();
        }
    }
}
