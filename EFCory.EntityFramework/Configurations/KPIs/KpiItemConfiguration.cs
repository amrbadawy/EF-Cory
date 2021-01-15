using EFCory.Entities.KPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace UPD.EntityFramework.Configurations
{
    public class KpiItemConfiguration : IEntityTypeConfiguration<KpiItem>
    {
        public void Configure(EntityTypeBuilder<KpiItem> builder)
        {
            builder.ToTable(TableName.KpiItems, SchemaName.KPI);
            builder.HasIndex(e => new { e.KpiAnnualItemId, e.KpiCode }).IsUnique();
            builder.Property(e => e.Value).IsRequired();

            builder.Property(rp => rp.CreatedAt).HasDefaultCurrentDate();
            builder.Property(rp => rp.UpdatedAt);

            builder
                .HasOne(e => e.KPI)
                .WithMany()
                .HasForeignKey(e => e.KpiCode)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.KpiAnnualItem)
                .WithMany(e => e.KpiItems)
                .HasForeignKey(e => e.KpiAnnualItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
