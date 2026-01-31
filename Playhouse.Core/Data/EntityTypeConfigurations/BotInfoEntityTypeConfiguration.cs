using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Core.Models;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BotInfo"/>.
    /// </summary>
    internal sealed class BotInfoEntityTypeConfiguration : IEntityTypeConfiguration<BotInfo>
    {
        public void Configure(EntityTypeBuilder<BotInfo> builder)
        {
            builder.ToTable("BotsInfo", t => t.HasComment("Информация и действия ботов."));
        }
    }
}
