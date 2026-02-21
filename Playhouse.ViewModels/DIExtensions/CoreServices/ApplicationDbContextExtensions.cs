using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playhouse.Core.Data;

namespace Playhouse.ViewModels.DIExtensions.CoreServices
{
    public static class ApplicationDbContextExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            return services.AddDbContextFactory<ApplicationDbContext>(
                (services, options) => options.UseSqlite(
                    services.GetRequiredService<IConfiguration>().GetConnectionString("ApplicationData")));
        }
    }
}
