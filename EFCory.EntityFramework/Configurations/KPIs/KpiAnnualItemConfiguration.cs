using EFCory.Entities.KPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UPD.EntityFramework.Configurations
{
    public class KpiAnnualItemConfiguration : IEntityTypeConfiguration<KpiAnnualItem>
    {
        public void Configure(EntityTypeBuilder<KpiAnnualItem> builder)
        {
            builder.ToTable(TableName.KpiAnnualItems, SchemaName.KPI);
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Year).IsUnique();

            builder.Property(rp => rp.CreatedAt).HasDefaultCurrentDate();

            builder
                .HasMany(e => e.KpiItems)
                .WithOne(e => e.KpiAnnualItem)
                .HasForeignKey(e => e.KpiAnnualItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
