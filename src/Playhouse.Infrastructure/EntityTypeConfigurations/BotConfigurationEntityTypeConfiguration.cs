using Ardalis.SmartEnum.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Domain;

namespace Playhouse.Infrastructure.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserConfiguration"/>.
    /// </summary>
    internal sealed class BotConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<BotConfiguration>
    {
        public void Configure(EntityTypeBuilder<BotConfiguration> builder)
        {
            builder.ToTable("Bot_Configurations", t => t.HasComment("Конфигурации ботов."));

            builder.Property(b => b.Browser)
                .HasConversion(new SmartEnumConverter<BrowserTypes, int>());
        }
    }
}
