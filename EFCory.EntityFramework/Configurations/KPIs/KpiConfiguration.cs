using EFCory.Entities.Blogs;
using EFCory.Entities.KPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UPD.EntityFramework.Configurations
{
    public class KpiConfiguration : IEntityTypeConfiguration<KPI>
    {
        public void Configure(EntityTypeBuilder<KPI> builder)
        {
            builder.ToTable(TableName.KPIs, SchemaName.KPI);
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).ValueGeneratedNever();

            builder.HasIndex(e => e.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(rp => rp.CreatedAt).HasDefaultCurrentDate();
        }
    }
}
