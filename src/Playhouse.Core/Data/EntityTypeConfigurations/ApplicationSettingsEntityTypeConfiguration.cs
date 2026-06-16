using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Core.Data.Comparers;
using Playhouse.Core.Data.Converters;
using Playhouse.Core.Services.ApplicationSettingsService;
using Playhouse.Domain;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    internal sealed class ApplicationSettingsEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationSettings>
    {
        public void Configure(EntityTypeBuilder<ApplicationSettings> builder)
        {
            builder.ToTable("Application_Settings", (b) => b.HasComment("Настройки приложения."));

            builder.Ignore(b => b.UICulture);

            builder.Property("_uiCultureCode");

            builder.Property(b => b.Browsers)
                .HasConversion(new HashSetSmartEnumToJsonConverter<BrowserTypes>(), new HashSetValueComparer<BrowserTypes>());

            builder.Property(c => c.Channels)
                .HasConversion(new HashSetSmartEnumToJsonConverter<BrowserChannels>(), new HashSetValueComparer<BrowserChannels>());
        }
    }
}
