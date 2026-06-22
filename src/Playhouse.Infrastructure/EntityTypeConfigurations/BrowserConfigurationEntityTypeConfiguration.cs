using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Domain;
using Playhouse.Infrastructure.Comparers;
using Playhouse.Infrastructure.Converters;

namespace Playhouse.Infrastructure.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserConfiguration"/>.
    /// </summary>
    internal sealed class BrowserConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<BrowserConfiguration>
    {
        public void Configure(EntityTypeBuilder<BrowserConfiguration> builder)
        {
            builder.ToTable("Browser_Configurations", t => t.HasComment("Конфигурации браузеров."));

            builder.OwnsOne(p => p.Options, b =>
            {
                b.Property(p => p.Args)
                    .HasConversion(new HashSetToJsonConverter<string>(), new HashSetValueComparer<string>());
            });

            builder.Property(b => b.UserVariables)
                .HasConversion(new HashSetToJsonConverter<Variable>(), new HashSetValueComparer<Variable>());
        }
    }
}
