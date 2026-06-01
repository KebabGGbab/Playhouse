using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Core.Models;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserConfiguration"/>.
    /// </summary>
    internal sealed class BrowserConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<BrowserConfiguration>
    {
        public void Configure(EntityTypeBuilder<BrowserConfiguration> builder)
        {
            builder.ToTable("Browser_Configurations", t => t.HasComment("Конфигурации браузеров."));
            builder.OwnsOne(p => p.Options);
        }
    }
}
