﻿using EFCory.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UPD.EntityFramework.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(TableName.Posts, SchemaName.AMR);
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Title).IsUnique();
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Content).HasMaxLength(2000).IsRequired();

            builder.Property(rp => rp.CreatedAt).HasDefaultCurrentDate();

            builder.HasMany(r => r.Tags)
               .WithMany(p => p.Posts)
               .UsingEntity<PostTag>(
                    rp => rp.ToTable(TableName.PostTags, SchemaName.AMR)
                        .HasOne(rp => rp.Tag)
                        .WithMany()
                        .HasForeignKey(rp => rp.TagId),
                    rp => rp
                        .HasOne(rp => rp.Post)
                        .WithMany()
                        .HasForeignKey(rp => rp.PostId),
                    rp =>
                    {
                        rp.Property(rp => rp.CreatedAt).HasDefaultCurrentDate();
                        rp.HasKey(rp => new { rp.PostId, rp.TagId });
                    });
        }
    }
}
