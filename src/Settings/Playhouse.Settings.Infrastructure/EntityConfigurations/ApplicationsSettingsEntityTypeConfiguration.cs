using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;
using Playhouse.SharedKernel.Infrastructure.Converters.SmartEnum;

namespace Playhouse.Settings.Infrastructure.EntityConfigurations
{
    internal class ApplicationsSettingsEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationSettings>
    {
        public void Configure(EntityTypeBuilder<ApplicationSettings> builder)
        {
            builder.ToTable("application_settings")
                .HasKey(a => a.Id);

            builder.Ignore(a => a.Events);

            builder.OwnsOne(
                a => a.Culture, 
                culture =>
                {
                    culture.Property(p => p.Code)
                        .HasColumnName("culture_name");
                });

            builder.OwnsOne(
                a => a.PathToData,
                pathToData =>
                {
                    pathToData.Property(p => p.Path)
                        .HasColumnName("path_to_data");
                });

            builder.Property(a => a.Browsers)
                .HasField("_browsers")
                .HasConversion<EnumerableSmartEnumConverter<BrowserType, int>, EnumerableValueComparer<BrowserType>>();

            builder.Property(a => a.Channels)
                .HasField("_channels")
                .HasConversion<EnumerableSmartEnumConverter<BrowserChannel, int>, EnumerableValueComparer<BrowserChannel>>();
        }
    }
}
