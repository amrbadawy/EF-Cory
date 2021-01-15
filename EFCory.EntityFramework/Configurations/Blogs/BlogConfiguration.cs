using EFCory.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UPD.EntityFramework.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable(TableName.Blogs, SchemaName.BLOG);
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(rp => rp.CreatedAt).HasDefaultCurrentDate();
        }
    }
}
