using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Core.Models;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserProfile"/>.
    /// </summary>
    internal sealed class BrowserProfileEntityTypeConfiguration : IEntityTypeConfiguration<BrowserProfile>
    {
        public void Configure(EntityTypeBuilder<BrowserProfile> builder)
        {
            builder.ToTable(t => t.HasComment("Настройки контекста браузера."));
            builder.OwnsOne(p => p.Options);
        }
    }
}
