using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    internal sealed class BrowserEventEntityTypeConfiguration : IEntityTypeConfiguration<BrowserEvent>
    {
        public void Configure(EntityTypeBuilder<BrowserEvent> builder)
        {
            builder.ToTable("BrowserEvents");
            builder
                .HasOne(e => e.BotInfo)
                .WithMany(e => e.BrowserEvents)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(e => e.BotInfoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
