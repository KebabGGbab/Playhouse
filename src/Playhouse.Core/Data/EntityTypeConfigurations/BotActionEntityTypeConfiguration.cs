using Ardalis.SmartEnum.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Playwright;
using Playhouse.Core.Data.Comparers;
using Playhouse.Core.Data.Converters;
using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BotAction"/>.
    /// </summary>
    internal sealed class BotActionEntityTypeConfiguration : IEntityTypeConfiguration<BotAction>
    {
        public void Configure(EntityTypeBuilder<BotAction> builder)
        {
            builder.ToTable("Bot_Actions");
            builder.HasOne(b => b.Bot)
                .WithMany(b => b.Actions)
                .HasPrincipalKey(b => b.Id)
                .HasForeignKey("BotId")
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserContextBotAction"/>.
    /// </summary>
    internal sealed class BrowserContextBotActionEntityTypeConfiguration : IEntityTypeConfiguration<BrowserContextBotAction>
    {
        public void Configure(EntityTypeBuilder<BrowserContextBotAction> builder)
        {
            builder.HasBaseType<BotAction>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserContextCreatedBotAction"/>.
    /// </summary>
    internal sealed class BrowserContextCreatedBotActionEntityTypeConfiguration : IEntityTypeConfiguration<BrowserContextCreatedBotAction>
    {
        public void Configure(EntityTypeBuilder<BrowserContextCreatedBotAction> builder)
        {
            builder.HasBaseType<BrowserContextBotAction>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserContextClosedBotAction"/>.
    /// </summary>
    internal sealed class BrowserContextClosedBotActionEntityTypeConfiguration : IEntityTypeConfiguration<BrowserContextClosedBotAction>
    {
        public void Configure(EntityTypeBuilder<BrowserContextClosedBotAction> builder)
        {
            builder.HasBaseType<BrowserContextBotAction>();
            builder.OwnsOne(c => c.Options);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageBotAction"/>.
    /// </summary>
    internal sealed class PageBotActionEntityTypeConfiguration : IEntityTypeConfiguration<PageBotAction>
    {
        public void Configure(EntityTypeBuilder<PageBotAction> builder)
        {
            builder.HasBaseType<BotAction>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageCreatedBotAction"/>.
    /// </summary>
    internal sealed class PageCreatedBotActionEntityTypeConfiguration : IEntityTypeConfiguration<PageCreatedBotAction>
    {
        public void Configure(EntityTypeBuilder<PageCreatedBotAction> builder)
        {
            builder.HasBaseType<PageBotAction>();
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageClosedBotAction"/>.
    /// </summary>
    internal sealed class PageClosedBotActionEntityTypeConfiguration : IEntityTypeConfiguration<PageClosedBotAction>
    {
        public void Configure(EntityTypeBuilder<PageClosedBotAction> builder)
        {
            builder.HasBaseType<PageBotAction>();
            builder.OwnsOne(p => p.Options);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="PageGoToBotAction"/>.
    /// </summary>
    internal sealed class PageGoToBotActionEntityTypeConfiguration : IEntityTypeConfiguration<PageGoToBotAction>
    {
        public void Configure(EntityTypeBuilder<PageGoToBotAction> builder)
        {
            builder.HasBaseType<PageBotAction>();
            builder.OwnsOne(p => p.Options);
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="LocatorBotAction"/>.
    /// </summary>
    internal sealed class LocatorBotActionEntityTypeConfiguration : IEntityTypeConfiguration<LocatorBotAction>
    {
        public void Configure(EntityTypeBuilder<LocatorBotAction> builder)
        {
            builder.HasBaseType<PageBotAction>();
            builder.OwnsOne(p => p.LocatorData, b =>
            {
                b.Property(p => p.Action)
                    .HasConversion(new SmartEnumConverter<ActionTypes, int>());
            });
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="LocatorClickBotAction"/>.
    /// </summary>
    internal sealed class LocatorClickBotActionEntityTypeConfiguration : IEntityTypeConfiguration<LocatorClickBotAction>
    {
        public void Configure(EntityTypeBuilder<LocatorClickBotAction> builder)
        {
            builder.HasBaseType<LocatorBotAction>();
            builder.OwnsOne(l => l.Options, b =>
            {
                b.Property(p => p.Modifiers)
                    .HasConversion(new HashSetEnumToJsonConverter<KeyboardModifier>(), new HashSetValueComparer<KeyboardModifier>());
            });
        }
    }

    /// <summary>
    /// Конфигурация типа сущности <see cref="LocatorClickBotAction"/>.
    /// </summary>
    internal sealed class LocatorFillBotActionEntityTypeConfiguration : IEntityTypeConfiguration<LocatorFillBotAction>
    {
        public void Configure(EntityTypeBuilder<LocatorFillBotAction> builder)
        {
            builder.HasBaseType<LocatorBotAction>();
            builder.OwnsOne(p => p.Options);
        }
    }
}
