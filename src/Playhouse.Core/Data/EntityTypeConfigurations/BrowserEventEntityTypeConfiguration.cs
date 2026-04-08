using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserEvent"/>.
    /// </summary>
    internal sealed class BrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<BrowserEvent>
    {
        public void Configure(EntityTypeBuilder<BrowserEvent> builder)
        {
            builder.ToTable("BrowserEvents");
            builder.HasOne(b => b.BotInfo)
                .WithMany(b => b.BrowserEvents)
                .HasPrincipalKey(b => b.Id)
                .HasForeignKey("BotInfoId")
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserContextBrowserEvent"/>.
    /// </summary>
    internal sealed class BrowserContextBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<BrowserContextBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<BrowserContextBrowserEvent> builder)
        {
            builder.HasBaseType<BrowserEvent>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserContextClosedBrowserEvent"/>.
    /// </summary>
    internal sealed class BrowserContextClosedBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<BrowserContextClosedBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<BrowserContextClosedBrowserEvent> builder)
        {
            builder.HasBaseType<BrowserContextBrowserEvent>();
            builder.OwnsOne(c => c.CloseOptions);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageBrowserEvent"/>.
    /// </summary>
    internal sealed class PageBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<PageBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<PageBrowserEvent> builder)
        {
            builder.HasBaseType<BrowserEvent>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageCreatedBrowserEvent"/>.
    /// </summary>
    internal sealed class PageCreatedBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<PageCreatedBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<PageCreatedBrowserEvent> builder)
        {
            builder.HasBaseType<PageBrowserEvent>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageClosedBrowserEvent"/>.
    /// </summary>
    internal sealed class PageClosedBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<PageClosedBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<PageClosedBrowserEvent> builder)
        {
            builder.HasBaseType<PageBrowserEvent>();
            builder.OwnsOne(p => p.CloseOptions);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageGoToBrowserEvent"/>.
    /// </summary>
    internal sealed class PageGoToBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<PageGoToBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<PageGoToBrowserEvent> builder)
        {
            builder.HasBaseType<PageBrowserEvent>();
            builder.OwnsOne(p => p.GotoOptions);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="LocatorBrowserEvent"/>.
    /// </summary>
    internal sealed class LocatorBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<LocatorBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<LocatorBrowserEvent> builder)
        {
            builder.HasBaseType<PageBrowserEvent>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="LocatorClickBrowserEvent"/>.
    /// </summary>
    internal sealed class LocatorClickBrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<LocatorClickBrowserEvent>
    {
        public void Configure(EntityTypeBuilder<LocatorClickBrowserEvent> builder)
        {
            builder.HasBaseType<LocatorBrowserEvent>();
            builder.OwnsOne(l => l.ClickOptions, b =>
            {
                b.OwnsOne(c => c.Position);
            });
        }
    }
}
